using StronglyTypedIds;
using System;

[assembly: StronglyTypedIdDefaults(
    backingType: StronglyTypedIdBackingType.Long,
    converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter |
                StronglyTypedIdConverter.Default | StronglyTypedIdConverter.TypeConverter,
    implementations: StronglyTypedIdImplementations.IEquatable | StronglyTypedIdImplementations.Default)]

namespace Boilerplate.Domain.Entities.Common;

public interface IGuid {}
public interface ILong { }

[StronglyTypedId]
public partial struct HeroId : ILong
{
    public static implicit operator HeroId(long heroId)
    {
        return new HeroId(heroId);
    }
}

[StronglyTypedId]
public partial struct UserId : ILong
{
    public static implicit operator UserId(long userId)
    {
        return new UserId(userId);
    }
}