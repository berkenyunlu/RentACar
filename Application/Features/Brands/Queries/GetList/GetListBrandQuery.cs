using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetList;

public class GetListBrandQuery : IRequest<GetListResponse<GetListBrandItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public class GetListBrandQueryHandler(IBrandRepository _brandRepository,IMapper _mapper) : IRequestHandler<GetListBrandQuery, GetListResponse<GetListBrandItemDto>>
    {
        public async Task<GetListResponse<GetListBrandItemDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            Paginate<Brand> brands = await _brandRepository.GetListAsync(
                  index: request.PageRequest.PageIndex,
                  size: request.PageRequest.PageSize,
                  cancellationToken: cancellationToken);
            GetListResponse<GetListBrandItemDto> response = _mapper.Map<GetListResponse<GetListBrandItemDto>>(brands);
            return response;
        }
    }
}