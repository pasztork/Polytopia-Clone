using Microsoft.AspNetCore.Components;
using WebServer.Data;

namespace WebServer.Helpers;

class TournamentValidator
{
    public string AlertClass = string.Empty;
    public MarkupString AlertMessage;

    public bool MapError { private get; set; } = false;
    public bool FilesError { private get; set; } = false;
    public bool TypeError { private get; set; } = false;
    public bool RoundCountError { private get; set; } = false;
    public bool KnockoutError { private get; set; } = false;
    public bool HasInfo { get; set; } = false;

    public bool HasError
    {
        get
        {
            return MapError || FilesError || TypeError || RoundCountError || KnockoutError;
        }
    }

    public void Reset()
    {
        MapError = false;
        FilesError = false;
        TypeError = false;
        RoundCountError = false;
        KnockoutError = false;
        HasInfo = false;
    }

    private void SetAlert(string alertClass, string iconClass, string message)
    {
        AlertClass = alertClass;
        AlertMessage = new MarkupString($"<span class='{iconClass}' aria-hidden='true'></span> {message}");
    }

    public bool Validate(FileModel? selectedMap, List<string> selectedZipFiles, TournamentType? tournamentType, int roundCount, int maxRoundCount)
    {
        Reset();
        if (selectedMap == null)
        {
            SetAlert(
                "alert alert-danger",
                "oi oi-warning",
                "Map not selected, please choose a <strong>map</strong> to start tournament.");
            MapError = true;
            return false;
        }

        if (selectedZipFiles.Count < 2)
        {
            SetAlert(
                "alert alert-danger",
                "oi oi-warning",
                "Not enough AI files selected, please choose <strong>at least two AI files</strong> to start tournament.");
            FilesError = true;
            return false;
        }
        else
        {
            FilesError = false;
        }

        if (tournamentType == null)
        {
            SetAlert(
                "alert alert-danger",
                "oi oi-warning",
                "Tournament type not selected, please choose <strong>tournament type</strong> to start tournament."
            );
            TypeError = true;
            return false;
        }

        if (tournamentType == TournamentType.League && (roundCount < 1 || roundCount > maxRoundCount))
        {
            SetAlert(
                "alert alert-danger",
                "oi oi-warning",
                $"Wrong match number, matches played againt each other must be <strong>between 1 and {maxRoundCount}</strong>."
            );
            RoundCountError = true;
            return false;
        }

        if (tournamentType == TournamentType.Knockout && (selectedZipFiles.Count & (selectedZipFiles.Count - 1)) != 0)
        {
            SetAlert(
                "alert alert-danger",
                "oi oi-warning",
                $"Wrong number of contestants to start knockout tournament, number of selected files must be <strong>the power of two</strong>."
            );
            KnockoutError = true;
            return false;
        }

        return true;
    }

    public void StartedAlert(int playerCount)
    {
        SetAlert(
            "alert alert-info",
            "oi oi-loop-circular",
            $"Tournament started with <strong>${playerCount}</strong> contestants."
        );
        HasInfo = true;
    }

    public void CompletedAlert(Dictionary<string, int> winners)
    {
        SetAlert(
            "alert alert-success",
            "oi oi-check",
            $"Tournament completed, the winner is <strong>{winners.First().Key}</strong>."
        );
        HasInfo = true;
    }
}
