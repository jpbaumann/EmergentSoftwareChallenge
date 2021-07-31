using EmergentSoftwareChallenge.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace EmergentSoftwareChallenge.Data
{
    public class SoftwareManager : ISoftwareManager
    {
        private static readonly Software[] _software = new Software[]
        {
            new()
            {
                Name = "MS Word",
                Version = "13.2.1."
            },
            new()
            {
                Name = "AngularJS",
                Version = "1.7.1"
            },
            new()
            {
                Name = "Angular",
                Version = "8.1.13"
            },
            new()
            {
                Name = "React",
                Version = "0.0.5"
            },
            new()
            {
                Name = "Vue.js",
                Version = "2.6"
            },
            new()
            {
                Name = "Visual Studio",
                Version = "2017.0.1"
            },
            new()
            {
                Name = "Visual Studio",
                Version = "2019.1"
            },
            new()
            {
                Name = "Visual Studio Code",
                Version = "1.35"
            },
            new()
            {
                Name = "Blazor",
                Version = "0.7"
            }
        };
        private readonly IVersionComparer _versionComparer;

        public SoftwareManager(IVersionComparer versionComparer)
        {
            _versionComparer = versionComparer ?? throw new ArgumentNullException(nameof(versionComparer));
        }

        public async IAsyncEnumerable<Software> GetAllSoftware([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var software in _software.ToAsyncEnumerable().WithCancellation(cancellationToken))
            {
                yield return software;
            }
        }

        public async IAsyncEnumerable<Software> GetSoftwareByVersion(
            string version,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var enumerable = _software.ToAsyncEnumerable().Where(x => _versionComparer.Compare(x.Version, version) >= 0);
            await foreach (var software in enumerable.WithCancellation(cancellationToken))
            {
                yield return software;
            }
        }
    }
}
