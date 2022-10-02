using AutoMapper;
using MetricsAgent.DAL.Models.Dto;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Request;

namespace MetricsAgent.Models.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricsCreateRequest, CpuMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<CpuMetric, CpuMetricsDto>();

            CreateMap<DotNetMetricsCreateRequest, DotNetMetrics>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<DotNetMetrics, DotNetMetricsDto>();

            CreateMap<HddMetricsCreateRequest, HddMetrics>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<HddMetrics, HddMetricsDto>();

            CreateMap<NetworkMetricsCreateRequest, NetworkMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<NetworkMetric, NetworkMetricsDto>();

            CreateMap<RamMetricsCreateRequest, RamMetrics>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<RamMetrics, RamMetricsDto>();
        }
    }
}
