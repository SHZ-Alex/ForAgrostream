using AutoMapper;
using Company.Delivery.Api.Controllers.Waybills.Request;
using Company.Delivery.Api.Controllers.Waybills.Response;
using Company.Delivery.Core;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Company.Delivery.Api.Controllers.Waybills;

/// <summary>
/// Waybills management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WaybillsController : ControllerBase
{
    private readonly IWaybillService _waybillService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Waybills management
    /// </summary>
    public WaybillsController(IWaybillService waybillService, IMapper mapper)
    {
        _waybillService = waybillService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получение Waybill
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено или кодом 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        try
        {
            WaybillDto waybill = await _waybillService.GetByIdAsync(id, cancellationToken);

            WaybillResponse response = _mapper.Map<WaybillResponse>(waybill);
            return Ok(response);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Создание Waybill
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] WaybillCreateRequest request, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если успешно создано
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        WaybillCreateDto dto = _mapper.Map<WaybillCreateDto>(request);
        WaybillDto result = await _waybillService.CreateAsync(dto, cancellationToken);

        WaybillResponse response = _mapper.Map<WaybillResponse>(result);
        return Ok(response);
    }

    /// <summary>
    /// Редактирование Waybill
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, [FromBody] WaybillUpdateRequest request, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено и изменено, или 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        try
        {
            WaybillUpdateDto ab = _mapper.Map<WaybillUpdateDto>(request);
            WaybillDto waybill = await _waybillService.UpdateByIdAsync(id, ab, cancellationToken);

            WaybillResponse response = _mapper.Map<WaybillResponse>(waybill);
            return Ok(response);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Удаление Waybill
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено и удалено, или 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        try
        {
            await _waybillService.DeleteByIdAsync(id, cancellationToken);
            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }
}