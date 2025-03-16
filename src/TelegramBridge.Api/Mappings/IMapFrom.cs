using System;
using AutoMapper;

namespace TelegramBridge.Api.Mappings;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
