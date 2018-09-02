using System;

namespace AiOption.Domain.Common {

    public interface IEntityTrackable {

        DateTimeOffset UpdatedDate { get; set; }
        DateTimeOffset CreatedDate { get; set; }

    }

}