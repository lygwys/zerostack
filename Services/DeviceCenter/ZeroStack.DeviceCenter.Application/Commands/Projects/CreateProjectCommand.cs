﻿using MediatR;
using System;
using ZeroStack.DeviceCenter.Application.Models.Projects;

namespace ZeroStack.DeviceCenter.Application.Commands.Projects
{
    public class CreateProjectCommand : IRequest<ProjectGetResponseModel>
    {
        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; }
    }
}
