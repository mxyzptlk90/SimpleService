namespace SimpleService.Data.InMemoryStorage
{
    public class InMemoryDataStorageWrapper<TValue>
    {
        private readonly Dictionary<int, TValue> _dataCollection = new();

        private int _maxIndex = 0;

        public int Add(TValue value) {
            int index = 0;

            // Simple way to emulate increasing Index
            lock (_dataCollection) {
                index = _maxIndex;
                _dataCollection.Add(index, value);
                _maxIndex++;
            }

            return index;
        }

        public TValue this[int key] {
            get {
                return _dataCollection[key];
            }
            set {
                _dataCollection[key] = value;
            }
        }

        public IEnumerable<TValue> ListAll() {
            return _dataCollection.Values.AsEnumerable();
        }

        public bool ContainsKey(int key) {
            return _dataCollection.ContainsKey(key);
        }

        public bool RemoveItem(int key) {
            return _dataCollection.Remove(key);
        }

        public int Count() {
            return _dataCollection.Count();
        }
    }
}
