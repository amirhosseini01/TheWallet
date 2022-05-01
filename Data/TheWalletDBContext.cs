using Microsoft.EntityFrameworkCore;
using TheWallet.Models;

namespace TheWallet.Data;

public class TheWalletDBContext : DbContext
{
    public TheWalletDBContext(DbContextOptions<TheWalletDBContext> options) : base(options)
    {
    }

    public DbSet<Finance> Finances { get; set; }
}