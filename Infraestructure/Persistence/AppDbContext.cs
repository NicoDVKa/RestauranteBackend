using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Infraestructure.Persistence
{
    public class AppDbContext : DbContext
    {

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<TipoMercaderia> TipoMercaderia { get; set; }
        public DbSet<Mercaderia> Mercaderia { get; set; }
        public DbSet<FormaEntrega> FormaEntrega { get; set; }
        public DbSet<Comanda> Comanda { get; set; }
        public DbSet<ComandaMercaderia> ComandaMercaderia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoMercaderia>(entity =>
            {
                entity.HasKey(tp => tp.TipoMercaderiaId);
                entity.Property(tp => tp.TipoMercaderiaId).ValueGeneratedOnAdd();
                entity.Property(tp => tp.Descripcion).HasMaxLength(50).IsRequired();


            });

            modelBuilder.Entity<Mercaderia>(entity =>
            {
                entity.HasKey(m => m.MercaderiaId);
                entity.Property(m => m.MercaderiaId).ValueGeneratedOnAdd();
                entity.Property(m => m.Nombre).HasMaxLength(50).IsRequired();
                entity.Property(m => m.Ingredientes).HasMaxLength(255).IsRequired();
                entity.Property(m => m.Preparacion).HasMaxLength(255).IsRequired();
                entity.Property(m => m.Imagen).HasMaxLength(255).IsRequired();

                // Relacion 1 a muchos con TipoMercaderia
                entity.HasOne<TipoMercaderia>(m => m.TipoMercaderia)
                      .WithMany(tp => tp.Mercaderias)
                      .HasForeignKey(m => m.TipoMercaderiaId);

            });

            modelBuilder.Entity<FormaEntrega>(entity =>
            {
                entity.HasKey(fe => fe.FormaEntregaId);
                entity.Property(fe => fe.FormaEntregaId).ValueGeneratedOnAdd();
                entity.Property(tp => tp.Descripcion).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Comanda>(entity =>
            {
                entity.HasKey(c => c.ComandaId);
                entity.Property(c => c.ComandaId).ValueGeneratedOnAdd();
                entity.Property(c => c.PrecioTotal).IsRequired();
                entity.Property(c => c.Fecha).IsRequired();

                // Relacion 1 a muchos con FormaEntrega
                entity.HasOne<FormaEntrega>(c => c.FormaEntrega)
                      .WithMany(fe => fe.Comandas)
                      .HasForeignKey(c => c.FormaEntregaId);
            });

            modelBuilder.Entity<ComandaMercaderia>(entity =>
            {

                entity.HasKey(cm => cm.ComandaMercaderiaId);
                entity.Property(cm => cm.ComandaMercaderiaId).ValueGeneratedOnAdd();

                // Relacion 1 a muchos con Comanda
                entity.HasOne<Comanda>(cm => cm.Comanda)
                      .WithMany(c => c.ComandaMercaderias)
                      .HasForeignKey(cm => cm.ComandaId);

                // Relacion 1 a muchos con Mercaderia
                entity.HasOne<Mercaderia>(cm => cm.Mercaderia)
                      .WithMany(m => m.ComandaMercaderias)
                      .HasForeignKey(cm => cm.MercaderiaId);

            });

            var tipoMercaderiaData = new TipoMercaderia[]
            {
                new TipoMercaderia
                {
                    TipoMercaderiaId = 1,
                    Descripcion = "Entrada"
                },
                new TipoMercaderia
                {
                    TipoMercaderiaId = 2,
                    Descripcion = "Minuta"
                },
                new TipoMercaderia
                {
                    TipoMercaderiaId = 3,
                    Descripcion = "Pastas"
                },
                new TipoMercaderia
                {
                    TipoMercaderiaId = 4,
                    Descripcion = "Parrilla"
                },
                new TipoMercaderia
                {
                    TipoMercaderiaId = 5,
                    Descripcion = "Pizzas"
                },
                new TipoMercaderia
                {
                    TipoMercaderiaId = 6,
                    Descripcion = "Sandwich"
                },
                new TipoMercaderia
                {
                    TipoMercaderiaId = 7,
                    Descripcion = "Ensalada"
                },
                new TipoMercaderia
                {
                    TipoMercaderiaId = 8,
                    Descripcion = "Bebidas"
                },
                new TipoMercaderia
                {
                    TipoMercaderiaId = 9,
                    Descripcion = "Cerveza Artesanal"
                },
                new TipoMercaderia
                {
                    TipoMercaderiaId = 10,
                    Descripcion = "Postres"
                }
            };

            modelBuilder.Entity<TipoMercaderia>().HasData(tipoMercaderiaData);

            var formaEntregaData = new FormaEntrega[]
            {
                new FormaEntrega
                {
                    FormaEntregaId = 1,
                    Descripcion = "Salon"
                },
                new FormaEntrega
                {
                    FormaEntregaId = 2,
                    Descripcion = "Delivery"
                },
                new FormaEntrega
                {
                    FormaEntregaId = 3,
                    Descripcion = "Pedidos Ya"
                }
            };

            modelBuilder.Entity<FormaEntrega>().HasData(formaEntregaData);


            var mercaderiaData = new Mercaderia[]
            {
                new Mercaderia
                {
                    MercaderiaId = 1 ,
                    Nombre = "deditos de queso",
                    TipoMercaderiaId = 1,
                    Precio = 600,
                    Ingredientes ="bastones de queso (6)",
                    Preparacion = "empanizar 6 bastones de queso y freirlos en abundante aceite",
                    Imagen = "https://drive.google.com/uc?export=download&id=14T7uOtb9yIaukf7EvPw99ntbb4A5M2q8",
                },
                new Mercaderia
                {
                    MercaderiaId = 2,
                    Nombre = "tortilla de papa" ,
                    TipoMercaderiaId = 2 ,
                    Precio = 1200,
                    Ingredientes = "papas (1), huevo (2), queso (100gr), jamon(100gr)" ,
                    Preparacion = "formar una tortilla de forma redonda y plana con huevo y trozos de papas, rellno de jamon y queso" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1tTY1nVA9cxIm4NyRcadOC1sQQMAN577w",
                },
                new Mercaderia
                {
                    MercaderiaId = 3,
                    Nombre = "sorrentinos de ricota" ,
                    TipoMercaderiaId = 3 ,
                    Precio = 1500,
                    Ingredientes = "crema (30gr), ricota (200gr)" ,
                    Preparacion = "pasta rellena en forma redonda con ricota y crema",
                    Imagen = "https://drive.google.com/uc?export=download&id=1JBK2cdZo-jimrJRYLYmMrvQQguh7PapL",
                },
                new Mercaderia
                {
                    MercaderiaId = 4,
                    Nombre = "entraña con papas fritas" ,
                    TipoMercaderiaId = 4 ,
                    Precio = 2600,
                    Ingredientes = "entrañas (1), limon (1), perejil fresco (50gr)" ,
                    Preparacion = "cocinar la entraña aderezada con limon y perejil en parrilla bien caliente con mucha brasa, freir las papas en aceite",
                    Imagen = "https://drive.google.com/uc?export=download&id=11VXVqMDgXd802UwA-u3PFtD-4MZDu5UA",
                },
                new Mercaderia
                {
                    MercaderiaId = 5,
                    Nombre = "pizza",
                    TipoMercaderiaId = 5 ,
                    Precio = 1300,
                    Ingredientes = "salsa de tomate (100cc), albahaca (7), mozzarella (500gr)",
                    Preparacion = "untar salsa de tomate a la masa, agregar la mozzarella y cocinar hasta grtinar el queso, añadir las hojas de albahaca",
                    Imagen = "https://drive.google.com/uc?export=download&id=1c4XjtSEudmSoPSzAbXoTatNJ_RJ_lYe6",
                },
                new Mercaderia
                {
                    MercaderiaId = 6,
                    Nombre = "sandwich club" ,
                    TipoMercaderiaId = 6 ,
                    Precio = 800,
                    Ingredientes = "rebanadas de pan (4), fetas de queso (2), filetes de pollo (2), lechuga (2hojas), tomate (4rebanadas), mayonesa (30gr)" ,
                    Preparacion = "tostar el pan, aderezarlo con mayonesa, colocar las fetas de queso, el tomate, los filetes de pollo y la lechuga" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1dR8NBAgnHhdwu1FrjO8uLUA3dqjbsZ_z",
                },
                new Mercaderia
                {
                    MercaderiaId = 7,
                    Nombre = "panzanella italiana" ,
                    TipoMercaderiaId = 7,
                    Precio = 600,
                    Ingredientes = "tomate cherry rojo (100gr), tomate cherry amarillo (100gr), tomate pera (1), cebolla morada (1), diente de ajo (1), aceite de oliva (45ml), vinagre (20ml)" ,
                    Preparacion = "cortar el tomate cherry rojo y amarillo por la mitad, cortar en rodajas el tomate pera y cortar la cebolla en juliana, picar el ajo y aderezar con vinagre y aceite de oliva" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=12wd13076D1W6sYiPPNzpRU-rr_44WQXj",
                },
                new Mercaderia
                {
                    MercaderiaId = 8,
                    Nombre = "mojito" ,
                    TipoMercaderiaId = 8 ,
                    Precio = 300,
                    Ingredientes = "azúcar (1cda), jugo de lima (30ml), menta fresca (5), ron blanco (60ml), cubos de hielo (4), soda (120ml)" ,
                    Preparacion = "en un vaso, mezclar las hojas de menta con el jugo de lima y el azúcar. Agregar el ron junto con los cubos de hielo. Luego, echar la soda y revolver" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1Gx6OtOL24pGD2eUbd0vdlBrX8hVEcRyG",
                },
                new Mercaderia
                {
                    MercaderiaId = 9,
                    Nombre = "cerveza a la miel",
                    TipoMercaderiaId = 9,
                    Precio = 600,
                    Ingredientes = "cerveza rubia (800cc), miel (2cdas), almíbar de frutas (2cdas)",
                    Preparacion = "en un vaso verter la cerveza rubia, colocar el almibar de frutas,la miel y revolver",
                    Imagen = "https://drive.google.com/uc?export=download&id=1mFg7nExEcLvb-TLd-CCXGtAqaE_B_ZQ4",
                },
                new Mercaderia
                {
                    MercaderiaId = 10,
                    Nombre = "crema de vainilla con cookies",
                    TipoMercaderiaId = 10,
                    Precio = 400,
                    Ingredientes = "leche (60ml), nata líquida (40ml), maizena (10gr), azúcar (10gr), mini galletas con pepitas de chocolate (3)",
                    Preparacion = "en una cacerola calentar la leche, el azucar y la nata con la maicena hasta espesar, dejar enfriar en una compotera y servir con las mini cookies",
                    Imagen = "https://drive.google.com/uc?export=download&id=1hv4T7tOZjNO7QFod12BYqBbrHRFEwPxi",
                },
                new Mercaderia
                {
                    MercaderiaId = 11,
                    Nombre = "empanadas caprese" ,
                    TipoMercaderiaId = 1 ,
                    Precio = 400,
                    Ingredientes = "parmesano rallado (100gr), mozzarella rallada (200gr),Tomates cherry cortados en cuartos (200gr), albahaca (4hojas)" ,
                    Preparacion = "cortamos la albahaca y la incorporamos en un bol junto a la mozzarella, el parmesano y los tomates cherry cortados en cuartos, salpimentamos, rellenamos las tapitas de empanadas y cocinamos" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1UNuJM9HQ1G06nC_O8Ewr_ccWBB-Rx2SE",
                },
                new Mercaderia
                {
                    MercaderiaId = 12,
                    Nombre = "canastatitas de calabaza" ,
                    TipoMercaderiaId = 2 ,
                    Precio = 500,
                    Ingredientes = "calabaza mediana (1), queso rallado (100gr), hierbas secas" ,
                    Preparacion = "hervir la calabaza para hacer un pure, mezclarla con queso rallado y las hierbas, formar canastas con tapitas de empanadas, rellenarlas y cocinar" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1ZHtIgaEBYu8yh_4JlSVtfzVgGua489q2",
                },
                new Mercaderia
                {
                    MercaderiaId = 13,
                    Nombre = "ñoquis de ricota" ,
                    TipoMercaderiaId = 3 ,
                    Precio = 1200,
                    Ingredientes = "ricota (500gr), huevo (1), harina (225gr)" ,
                    Preparacion = "colocar en un bol la ricota y el huevo, salpimentar, mezclar e integrar la harina, formar la masa y cortarla en cubitos y cocinarlos en agua hirviendo hasta que floten" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1u3ymdD7Yk8cP3LsRk39cl9i3wd13ch7Z",
                },
                new Mercaderia
                {
                    MercaderiaId =14 ,
                    Nombre = "matambre a la parrilla" ,
                    TipoMercaderiaId = 4 ,
                    Precio = 3000,
                    Ingredientes = "matambre (1), leche (1000ml), ajo (1), laurel (4)" ,
                    Preparacion = "macerar el matambre durante una noche cubierto de leche, las hojas de laurel y el ajo, llevar a una parrilla a fuego medio del lado de la grasa hasta que esté dorado, dar vuelta y dorar del lado restante" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1pJNiFNZLp7B_vTCex_4Cslgw8_nFk-KG",
                },
                new Mercaderia
                {
                    MercaderiaId = 15 ,
                    Nombre = "pizza hawaiana" ,
                    TipoMercaderiaId = 5 ,
                    Precio = 1300,
                    Ingredientes = "anana (4rodajas), queso rallado (200gr), jamon cocido (400gr), queso mozzarella (300gr), salsa de tomate (250ml)" ,
                    Preparacion = "cortar en trozos el jamón y en cubos el anana, untamos la masa con salsa de tomate, cubrir con el queso rallado, repartir por encima el jamón cocido y el anana y cocinar" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1tph3tQXtdVtNjcIaaNXJMTawKemEvf1C",
                },
                new Mercaderia
                {
                    MercaderiaId = 16 ,
                    Nombre = "sandwich cubano" ,
                    TipoMercaderiaId = 6 ,
                    Precio = 700,
                    Ingredientes = "rebanadas de pan (4), jamon cocido (2fetas), salami (2fetas), lomo de cerdo asado (2lonchas), mostaza (10gr) " ,
                    Preparacion = "cortamos los panes por la mitad, untarlos con mostaza y colocar el jamón, el salami, el lomo, el queso" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1sgRyeSS2z8dr39_3ZPeRCRRTkfISfFqz",
                },
                new Mercaderia
                {
                    MercaderiaId = 17 ,
                    Nombre = "ensalada multicolor" ,
                    TipoMercaderiaId = 7 ,
                    Precio = 500,
                    Ingredientes = "lechuga (1), cebolla morada (1), zanahoria (1), tomates cherrys rojos (4)" ,
                    Preparacion = "cortar la lechuga y la cebolla morada en tiras, rallar la zanahoria, cortar los tomates por la mitad, aderezar y salpimentar" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1FtxE8-4g7XZ5Ltju1RqcNXFiE5g4QbqX",
                },
                new Mercaderia
                {
                    MercaderiaId = 18 ,
                    Nombre = "jugo de melon" ,
                    TipoMercaderiaId = 8 ,
                    Precio = 300,
                    Ingredientes = "melon (1/2), agua fria (1000ml), azucar (20gr), hielo (7)" ,
                    Preparacion = "en una licuadora agregar el melon cortado en cubos, el agua y el azucar, licuar hasta que quede homgeneo, servir en una jarra con los hielos" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1l2ywuMdj-xuVOc-QOJFClgOWRxIPu4Xy",
                },
                new Mercaderia
                {
                    MercaderiaId = 19 ,
                    Nombre = "cerveza con gin y limon" ,
                    TipoMercaderiaId = 9 ,
                    Precio = 900 ,
                    Ingredientes = "cerveza rubia (1 lata), gin (1 medida), jugo de limon (1 medida)" ,
                    Preparacion = "en un vaso verter la cerveza rubia, el gin y el jugo de limon y revolver" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=18svxxGvBjCxuehiTiMwp9aqhTXe_7BQ5",
                },
                new Mercaderia
                {
                    MercaderiaId = 20 ,
                    Nombre = "crema de leche merengada" ,
                    TipoMercaderiaId = 10 ,
                    Precio = 500,
                    Ingredientes = "leche (60ml), claras de huevo (50ml), sobre de gelatina sin sabor (1), azúcar (10gr)" ,
                    Preparacion = "hervir la leche con el azúcar, disolvemos la gelatina en la mezcla, batir las claras a punto de nieve con un poco de azúcar, incorporar las claras y batir hasta que se integre todo y dejar enfriar en la heladera" ,
                    Imagen = "https://drive.google.com/uc?export=download&id=1RtTHSy7oWWv3pYMGib8RS0FhKvFpczz_",
                }
            };

            modelBuilder.Entity<Mercaderia>().HasData(mercaderiaData);
        }

    }
}
