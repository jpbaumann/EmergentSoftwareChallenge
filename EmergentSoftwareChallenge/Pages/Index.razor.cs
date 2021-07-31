using EmergentSoftwareChallenge.Data;
using EmergentSoftwareChallenge.Infrastructure;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmergentSoftwareChallenge.Pages
{
    public partial class Index
    {
        private string _filterText;

        [Inject] private ISoftwareManager SoftwareManager { get; set; }
        [Inject] private IVersionComparer VersionComparer { get; set; }
        private IEnumerable<Software> Software { get; set; }
        private SortDirection? SortDirection { get; set; }
        private string SortColumnName { get; set; }

        private string GetFilterAdorner() =>
            SortDirection switch
            {
                Infrastructure.SortDirection.Ascending => "fas fa-sort-up",
                Infrastructure.SortDirection.Descending => "fas fa-sort-down",
                _ => string.Empty,
            };

        protected override async Task OnInitializedAsync()
        {
            Software = await SoftwareManager.GetAllSoftware()
                .OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase)
                .ThenBy(x => x.Version, VersionComparer)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        private async Task OnFilterChanged(ChangeEventArgs args)
        {
            var value = args.Value as string;
            if (string.Equals(_filterText, value))
                return;
            _filterText = value;
            await SortSoftware(SoftwareManager.GetSoftwareByVersion(value)).ConfigureAwait(false);
        }

        private async Task OnHeaderClick(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                SortColumnName = default;
                SortDirection = default;
                return;
            }
            if (string.Equals(SortColumnName, columnName))
            {
                SortDirection = SortDirection == Infrastructure.SortDirection.Ascending
                    ? Infrastructure.SortDirection.Descending
                    : Infrastructure.SortDirection.Ascending;
            }
            else
            {
                SortColumnName = columnName;
                SortDirection = Infrastructure.SortDirection.Ascending;
            }
            await SortSoftware(Software.ToAsyncEnumerable()).ConfigureAwait(false);
        }

        private async Task SortSoftware(IAsyncEnumerable<Software> software)
        {
            Func<Software, string> selector;
            IComparer<string> comparer = StringComparer.InvariantCultureIgnoreCase;
            switch (SortColumnName)
            {
                case nameof(Data.Software.Name):
                    selector = x => x.Name;
                    break;
                case nameof(Data.Software.Version):
                    selector = x => x.Version;
                    comparer = VersionComparer;
                    break;
                default:
                    Software = await software.ToArrayAsync().ConfigureAwait(false);
                    return;
            }
            if (SortDirection == Infrastructure.SortDirection.Ascending)
            {
                Software = await software.OrderBy(selector, comparer).ToArrayAsync().ConfigureAwait(false);
                return;
            }
            Software = await software.OrderByDescending(selector, comparer).ToArrayAsync().ConfigureAwait(false);
        }
    }
}
