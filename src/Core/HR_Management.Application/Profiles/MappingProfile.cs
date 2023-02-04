﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management.Application.DTOs;
using HR_Management.Domain;

namespace HR_Management.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveAllocation,LeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveRequest,LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveType,LeaveTypeDto>().ReverseMap();
            CreateMap<LeaveAllocation,LeaveAllocationDto>().ReverseMap();
        }
    }
}