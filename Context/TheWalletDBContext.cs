using Microsoft.EntityFrameworkCore;
using TheWallet.Models;

namespace TheWallet.Context;

public class TheWalletDBContext : DbContext
{
    public TheWalletDBContext(DbContextOptions<TheWalletDBContext> options) : base(options)
    {
    }

    public DbSet<Finance> Cars { get; set; } = default!;
}