using System.Collections.Generic;

public class Team
{
    private string name;

    private List<Player> players;

    public Team(string name)
    {
        this.Name = name;
        players = new List<Player>();
    }

    public string Name
    {
        get
        {
            return this.name;
        }
        set
        {
            if (Validator.IsCurrentNameNullEmptyOrWhiteSpase(value))
            {
                Messages.InvalidNameValueException();
            }
            this.name = value;
        }
    }

    public double Rating
    {
        get
        {
            return MathOptions.Rating(this.players);
        }
    }

    public int PlayersCount
    {
        get
        {
            return this.players.Count;
        }
    }

    public void Add(Player player)
    {
        if (Validator.PlayerDoseNotExistInThisTeam(player.Name, this.players))
        {
            this.players.Add(player);
        }
    }

    public void Remove(string player)
    {
        if (Validator.PlayerDoseNotExistInThisTeam(player, this.players))
        {
            Messages.PlayerNotExistException(player, this.name);
        }
        var currentPlayer = this.players.Find(p => p.Name == player);
        this.players.Remove(currentPlayer);
    }
}