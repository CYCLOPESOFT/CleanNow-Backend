using CleanNow.Core.Application.Dto.DetailsDomicile;
using CleanNow.Core.Application.Features.DetailsDomiciles.Commands.CreateDetailsDomicile;
using CleanNow.Core.Application.Features.DetailsDomiciles.Commands.DeleteDetailsDomicielById;
using CleanNow.Core.Application.Features.DetailsDomiciles.Commands.UpdateDetailsDomicile;
using CleanNow.Core.Application.Features.DetailsDomiciles.Queries.GetAllDetailsDomicile;
using CleanNow.Core.Application.Features.DetailsDomiciles.Queries.GetDetailsDomicileById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanNow.Controllers.V1
{
    [ApiVersion("1.0")]
    public class DetailsDomicileController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(GetDetailsDomicileDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllDetailsDomicileQuery()));
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDetailsDomicileDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetById(int id)
        {
            try 
            {
                return Ok(await Mediator.Send(new GetDetailsByIdQuery { Id = id}));
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Create ([FromBody]CreateDetailsDomicileCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await Mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailsDomicileUpdateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Update(int id, [FromBody]UpdateDetailsDomicileCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if(id != command.Id)
                {
                    return BadRequest();
                }
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(GetDetailsDomicileDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteDetailsDomicileCommand { Id = id});
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

    }
}
