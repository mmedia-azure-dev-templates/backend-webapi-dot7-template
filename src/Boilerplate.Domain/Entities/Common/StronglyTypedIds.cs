using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StronglyTypedIds;
using System;

[assembly: StronglyTypedIdDefaults(
    backingType: StronglyTypedIdBackingType.Guid,
    converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter |
                StronglyTypedIdConverter.Default | StronglyTypedIdConverter.TypeConverter,
    implementations: StronglyTypedIdImplementations.IEquatable | StronglyTypedIdImplementations.Default)]

namespace Boilerplate.Domain.Entities.Common;

public interface IGuid {}
public interface ILong { }
public interface IInt { }
public interface IString { }

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid)]
public partial struct ArticleId : IGuid
{
    public static implicit operator ArticleId(Guid articleId)
    {
        return new ArticleId(articleId);
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Long)]
public partial struct CatalogId : ILong
{
    public static implicit operator CatalogId(long catalogId)
    {
        return new CatalogId(catalogId);
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Long)]
public partial struct ContactId : ILong
{
    public static implicit operator ContactId(long contactId)
    {
        return new ContactId(contactId);
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Long)]
public partial struct CounterId : ILong
{
    public static implicit operator CounterId(long counterId)
    {
        return new CounterId(counterId);
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int)]
public partial struct GeographicLocationId : IInt
{
    public static implicit operator GeographicLocationId(int geographicLocationId)
    {
        return new GeographicLocationId(geographicLocationId);
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Long)]
public partial struct HeroId : ILong
{
    public static implicit operator HeroId(long heroId)
    {
        return new HeroId(heroId);
    }
}

[StronglyTypedId]
public partial struct IdentificationId : IGuid
{
    public static implicit operator IdentificationId(Guid identificationId)
    {
        return new IdentificationId(identificationId);
    }
}

public readonly partial struct IdentificationId
{
    public class StronglyTypedIdEfValueConverter : ValueConverter<IdentificationId, Guid>
    {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints mappingHints = null)
            : base(id => id.Value, value => new IdentificationId(value), mappingHints)
        {
        }
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Long)]
public partial struct InscriptionId : ILong
{
    public static implicit operator InscriptionId(long inscriptionId)
    {
        return new InscriptionId(inscriptionId);
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Long)]
public partial struct InventoryDocId : ILong
{
    public static implicit operator InventoryDocId(long inventoryDocId)
    {
        return new InventoryDocId(inventoryDocId);
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Long)]
public partial struct OrderId : ILong
{
    public static implicit operator OrderId(long orderId)
    {
        return new OrderId(orderId);
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Long)]
public partial struct PostulantId : ILong
{
    public static implicit operator PostulantId(long postulantId)
    {
        return new PostulantId(postulantId);
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Long)]
public partial struct ProductId : ILong
{
    public static implicit operator ProductId(long productId)
    {
        return new ProductId(productId);
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid)]
public partial struct UserId : IGuid
{
    public static implicit operator UserId(Guid userId)
    {
        return new UserId(userId);
    }
}

public readonly partial struct UserId
{
    public class StronglyTypedIdEfValueConverter : ValueConverter<UserId, Guid>
    {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints mappingHints = null)
            : base(id => id.Value, value => new UserId(value), mappingHints)
        {
        }
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid)]
public partial struct TeamId : IGuid
{
    public static implicit operator TeamId(Guid teamId)
    {
        return new TeamId(teamId);
    }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid)]
public partial struct OrderItemId : IGuid
{
    public static implicit operator OrderItemId(Guid orderItemId)
    {
        return new OrderItemId(orderItemId);
    }
}