﻿using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest<DeletedBrandResponse>
{
    public Guid Id { get; set; }
    public class DeleteBrandCommandHandler(IBrandRepository _brandRepository, IMapper _mapper) : IRequestHandler<DeleteBrandCommand, DeletedBrandResponse>
    {
        public async Task<DeletedBrandResponse> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _brandRepository.DeleteAsync(brand);
            DeletedBrandResponse response = _mapper.Map<DeletedBrandResponse>(brand);
            return response;
        }
    }
}