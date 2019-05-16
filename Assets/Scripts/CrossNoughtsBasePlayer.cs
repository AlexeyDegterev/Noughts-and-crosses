public class CrossNoughtsBasePlayer
{
    private string _sName;
    public string sName
    {
        get
        {
            return _sName;
        }
        set
        {
            _sName = value;
        }
    }


    private bool _bIsThisTypeIsCrosses;
    public bool bIsThisTypeIsCrosses
    {
        get
        {
            return _bIsThisTypeIsCrosses;
        }
        set
        {
            _bIsThisTypeIsCrosses = value;
        }
    }

    public CrossNoughtsBasePlayer(string sName, bool bIsThisACrosses)
    {
        _sName = sName;
        _bIsThisTypeIsCrosses = bIsThisACrosses;
    }
}
