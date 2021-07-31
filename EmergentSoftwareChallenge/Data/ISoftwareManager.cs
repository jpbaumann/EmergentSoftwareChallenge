using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace EmergentSoftwareChallenge.Data
{
    public interface ISoftwareManager
    {
        IAsyncEnumerable<Software> GetAllSoftware(CancellationToken cancellationToken = default);
        IAsyncEnumerable<Software> GetSoftwareByVersion(string version, [EnumeratorCancellation] CancellationToken cancellationToken = default);
    }
}