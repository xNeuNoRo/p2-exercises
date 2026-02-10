using App.Models;

namespace App.Domain.Infrastructure;

public class Database
{
    private readonly List<Admin> _admins;
    private readonly List<Client> _clients;
    private readonly List<Guest> _guests;

    public Database()
    {
        _admins = new List<Admin>();
        _clients = new List<Client>();
        _guests = new List<Guest>();
    }

    // Metodos para acceder a la listaS
    public List<Admin> Admins => _admins;
    public List<Client> Clients => _clients;
    public List<Guest> Guests => _guests;
}
