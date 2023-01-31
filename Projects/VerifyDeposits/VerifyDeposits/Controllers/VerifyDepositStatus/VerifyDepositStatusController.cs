using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VerifyDeposits.Repositories.DepositStatusRepository;

namespace VerifyDeposits.Controllers.VerifyDepositStatus
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VerifyDepositStatusController : ControllerBase
    {
        private readonly ILogger<VerifyDepositStatusController> _logger;
        private readonly IDepositStatusRepository _depositStatusRepository;

        public VerifyDepositStatusController(ILogger<VerifyDepositStatusController> logger, IDepositStatusRepository depositStatusRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _depositStatusRepository = depositStatusRepository ?? throw new ArgumentNullException(nameof(depositStatusRepository));
        }

        [HttpGet, Route("verifyAllDeposits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult VerifyAllDepositsAsync()
        {
            try
            {
                var output = _depositStatusRepository.GetAllDepositsStatusAsync();

                if (output == null)
                {
                    return NoContent();
                }
                return Ok(output);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro em VerifyAllDepositsAsync. Mensagem: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet, Route("verifyDeposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VerifyDepositAsync([Required(ErrorMessage = "O campo Conta Destino é obrigatório.")] string contaDestino)
        {
            try
            {
                var output = await _depositStatusRepository.GetDepositStatusByAccountAsync(contaDestino);

                if (output == null)
                {
                    return NoContent();
                }
                return Ok(output);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro em VerifyDepositAsync. Mensagem: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}