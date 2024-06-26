﻿using MediatR;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace Authentication.Infrastructure.Features.HealthCheck
{
    public class HealthCheckQueryHandler : IRequestHandler<HealthCheckQuery, Result<object>>
    {
        public async Task<Result<object>> Handle(HealthCheckQuery request, CancellationToken cancellationToken)
        {
            return await Result<object>.SuccessAsync(request.Message);
        }
    }
}
