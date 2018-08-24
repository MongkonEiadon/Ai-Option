using System;

namespace AiOption.Infrastructure.DataAccess {

    public interface IEntityTrackable {

        DateTimeOffset UpdatedDate { get; set; }
        DateTimeOffset CreatedDate { get; set; }

    }

}