using AutoMapper;
using Domain.Entities;

namespace Application.Models;

public record UrlLocation
(
    string FullUrl,
    string Location
);