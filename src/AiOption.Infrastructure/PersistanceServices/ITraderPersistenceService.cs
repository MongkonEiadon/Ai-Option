using System;

using IqOptionApi.Models;

namespace AiOption.Infrastructure.PersistanceServices {

    public interface ITraderPersistenceService {

        IObservable<InfoData> TraderOpenPositionStream { get; }

    }


}