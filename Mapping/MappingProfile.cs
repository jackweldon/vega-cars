using AutoMapper;
using vega.Controllers.Resources;
using vega.Core.Models;
using System.Linq;
using System.Collections.Generic;

namespace vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //domain to API resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Features, KeyValuePairResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Photo,PhotoResource>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Vehicle, SaveVehicleResource>().ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource
            {
                Name = v.ContactEmail,
                Email = v.ContactEmail,
                Phone = v.ContactPhone
            })).ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            CreateMap<Vehicle, VehicleResource>()
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource
            {
                Name = v.ContactEmail,
                Email = v.ContactEmail,
                Phone = v.ContactPhone
            }))
            .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
            .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource { Id = vf.Feature.Id, Name = vf.Feature.Name })));

            //api reource to domain.
            CreateMap<SaveVehicleResource, Vehicle>()
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(m => m.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(m => m.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
            .ForMember(m => m.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(m => m.Features, opt => opt.Ignore())
            .AfterMap((vr, v) =>
            {
                //remove unselected features
                var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));
                foreach (var f in removedFeatures)
                    v.Features.Remove(f);

                //add selected features
                var added = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id });
                foreach (var f in added)
                    v.Features.Add(f);

            });

            CreateMap<VehicleQueryResource,VehicleQuery>();


        }

    }
}