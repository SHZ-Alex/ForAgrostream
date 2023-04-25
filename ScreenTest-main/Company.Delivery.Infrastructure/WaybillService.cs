using AutoMapper;
using Company.Delivery.Core;
using Company.Delivery.Database;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Dto;
using Microsoft.EntityFrameworkCore;

namespace Company.Delivery.Infrastructure;

public class WaybillService : IWaybillService
{
    private readonly DeliveryDbContext _db;
    private readonly IMapper _mapper;

    public WaybillService(DeliveryDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<WaybillDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        Waybill? waybill = await _db.Waybills.Include(u => u.Items).FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (waybill == null)
            throw new EntityNotFoundException();

        return _mapper.Map<WaybillDto>(waybill);
    }

    public async Task<WaybillDto> CreateAsync(WaybillCreateDto data, CancellationToken cancellationToken)
    {
        Waybill waybill = _mapper.Map<Waybill>(data);

        await _db.Waybills.AddAsync(waybill, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        WaybillDto request = _mapper.Map<WaybillDto>
            (await _db.Waybills.FirstOrDefaultAsync(x => x.Number == data.Number, cancellationToken: cancellationToken));

        return request;
    }

    public async Task<WaybillDto> UpdateByIdAsync(Guid id, WaybillUpdateDto data, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException

        Waybill? waybill = await _db.Waybills.Include(u => u.Items).FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (waybill is null)
            throw new EntityNotFoundException();

        Waybill obj = _mapper.Map<Waybill>(data);
        waybill.Items = obj.Items;
        waybill.Number = obj.Number;
        waybill.Date = obj.Date;

        _db.Waybills.Update(waybill);
        await _db.SaveChangesAsync(cancellationToken);

        return _mapper.Map<WaybillDto>(waybill);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        Waybill? waybill = await _db.Waybills.Include(u => u.Items).FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (waybill is null)
            throw new EntityNotFoundException();

        _db.Waybills.Remove(waybill);
        await _db.SaveChangesAsync(cancellationToken);
    }
}