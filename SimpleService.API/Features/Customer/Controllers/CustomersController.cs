using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleService.API.Features.Customer.CQRS.Commands;
using SimpleService.API.Features.Customer.CQRS.Queries;
using SimpleService.API.Features.Customer.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SimpleService.API.Features.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IMediator mediator, ILogger<CustomersController> logger) {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation("Get all customers")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<CustomerModelApi>))]
        public async Task<ActionResult<IEnumerable<CustomerModelApi>>> Get() {
            try {
                _logger.LogInformation("Request to get all customers...");
                var customers = await _mediator.Send(new GetCustomersQuery());

                return Ok(customers);
            } catch(Exception e) {
                _logger.LogError(e, "Failed to process get all request...");
                throw;
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [SwaggerOperation("Get customer by id")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(CustomerModelApi))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerModelApi>> Get([FromRoute] int id) {
            try {
                _logger.LogInformation($"Request to get customer with id {id}..");
                var customer = await _mediator.Send(new GetCustomerQuery { Id = id });

                if (customer != null) {
                    return Ok(customer);
                } else {
                    _logger.LogInformation($"Customer with id {id} was not found");
                    return NotFound();
                }
            } catch(Exception e) {
                _logger.LogError(e, $"Failed to get customer with id {id}");
                throw;
            }
}

        [HttpPost]
        [SwaggerOperation("Create new customer")]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(CustomerModelApi))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerModelApi>> Post([FromBody] CreateCustomerRequest model) {
            try {
                _logger.LogInformation("Request to create new customer. Request body: {@Model}", model);
                var newCustomer = await _mediator.Send(new CreateCustomerCommand { Model = model });

                _logger.LogInformation("New customer with id {Id} was created.", newCustomer.Id);
                return Created($"{Request.Path}/{newCustomer.Id}", newCustomer);
            }
            catch (Exception e) {
                _logger.LogError(e, "Failed to create customer with parameters: {@Model}", model);
                throw;
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [SwaggerOperation("Delete customer by id")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] int id) {
            try {
                _logger.LogInformation($"Request to delete customer with id {id}");
                var deleted = await _mediator.Send(new DeleteCustomerCommand { Id = id });

                if (deleted) {
                    _logger.LogInformation($"Customer with id {id} was deleted");
                    return Ok();
                } else {
                    _logger.LogInformation($"Customer with id {id} was not found");
                    return NotFound();
                }
            } catch (Exception e) {
                _logger.LogError(e, $"Failed to delet customer with id {id}");
                throw;
            }
        }
    }
}
