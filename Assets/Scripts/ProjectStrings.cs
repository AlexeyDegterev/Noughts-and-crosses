using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ProjectStrings
{
    static string sLANGUAGE = "RUS";

    static string sResultsWinCrosses_rus = "Победили крестики!";
    static string sResultsWinNoughts_rus = "Победили нолики!";
    static string sResultsStalemate_rus = "Ничья!";

    public static string GetStringResWinCrosses()
    {
        if (sLANGUAGE == "RUS")
            return sResultsWinCrosses_rus;
        return "";
    }
    public static string GetStringResWinNoughts()
    {
        if (sLANGUAGE == "RUS")
            return sResultsWinNoughts_rus;
        return "";
    }
    public static string GetStringResStalemate()
    {
        if (sLANGUAGE == "RUS")
            return sResultsStalemate_rus;
        return "";
    }

    static string sTurnCrosses_rus = "Ходят крестики";
    static string sTurnNoughts_rus = "Ходят нолики";
    public static string GetStringTurnCrosses()
    {
        if (sLANGUAGE == "RUS")
            return sTurnCrosses_rus;
        return "";
    }
    public static string GetStringTurnNoughts()
    {
        if (sLANGUAGE == "RUS")
            return sTurnNoughts_rus;
        return "";
    }

    static string sCrosses_rus = "крестики";
    static string sNoughts_rus = "нолики";
    public static string GetStringCrosses()
    {
        if (sLANGUAGE == "RUS")
            return sCrosses_rus;
        return "";
    }
    public static string GetStringNoughts()
    {
        if (sLANGUAGE == "RUS")
            return sNoughts_rus;
        return "";
    }
}

