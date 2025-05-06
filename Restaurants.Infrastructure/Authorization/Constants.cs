namespace Restaurants.Infrastructure.Authorization;


public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string AtLeast20 = "AtLeast20";
    public const string OwnsTwoRestaurants = "OwnsTwoRestaurants";

}

public static class AppClaimsTypes
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
}