using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monopoly;


public static class ObjectReferences
{

    #region Public Variables
    public const float PLAYER_SPEED = 4.0f;

    public const int GO_MONEY = 200;
    public const string PLAYER_ID_PREFIX = "Player ";
    public const int GO_ID = 0;
    public const int FREE_PARKING_ID = 1;
    public const int JAIL_ID = 2;
    public const int VISIT_JAIL_ID = 3;
    public const int COMMUNITY_ID = 4;
    public const int CHANCE_ID = 5;
    public const int TAX_ID = 6;
    public const int STATION_ID = 7;
    public const int UTILITIES_ID = 8;

    public const int BLUE_PROPERTY_ID = 10;
    public const int RED_PROPERTY_ID = 10;
    public const int ORANGE_PROPERTY_ID = 10;
    public const int GREY_PROPERTY_ID = 10;
    public const int LIGHT_SKY_PROPERTY_ID = 10;
    public const int PURPLE_PROPERTY_ID = 10;
    public const int YELLOW_PROPERTY_ID = 10;
    public const int GREEN_PROPERTY_ID = 10;

    public const string COLOUR_BLUE = "BLUE";
    public const string COLOUR_RED = "RED";
    public const string COLOUR_ORANGE = "ORANGE";
    public const string COLOUR_LIGHT_SKY = "LIGHTSKY";
    public const string COLOUR_BROWN = "BROWN";
    public const string COLOUR_YELLOW = "YELLOW";
    public const string COLOUR_PURPLE = "PURPLE";
    public const string COLOUR_GREEN = "GREEN";
    public const string COLOUR_WATER = "WATER";
    public const string COLOUR_RAILSTATION = "RAIL";

    /// <summary>
    ///Runnning from Lcoal machine using this path variables
    /// </summary>
    /*
    public const string PATH1 = "http://localhost/Gamesite/data/PropertyInformation.php";
    public const string PATH2 = "http://localhost/Gamesite/data/MonopolyChance.php";
    public const string PATH3 = "http://localhost/Gamesite/data/MonopolyCommunity.php";
    */

    /// <summary>
    ///Runnning from website  using this path variables
    /// </summary>
    public const string PATH1 = "https://joni.lukejt.com/data/PropertyInformation";
    public const string PATH2 = "https://joni.lukejt.com/data/MonopolyChance";
    public const string PATH3 = "https://joni.lukejt.com/data/MonopolyCommunity";

    public static bool IsProperty(int ID) {

        return (ID == 10 || ID == 11 || ID == 12 || ID == 13 || ID == 14 ||
                    ID == 15 || ID == 16 || ID == 17) ? true : false;
    }

    #endregion


}
