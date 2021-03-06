using System;
using MongoDB.Bson;

namespace Bazaar.Common.Profiles
{
    public interface IProfile
    {
        ObjectId _id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string Photo { get; set; }
        string Culture { get; set; }
        string Location { get; set; }
        string CallingCode { get; set; }
        string PhoneNumber { get; set; }
        string IdentityName { get; set; }
        string NormalizedName { get; set; }
        Gender Gender { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }  
        DateTime? DeletedAt { get; set; }
        DateTime? Birthday { get; set; }
    }
}