using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormaEntrega",
                columns: table => new
                {
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaEntrega", x => x.FormaEntregaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoMercaderia",
                columns: table => new
                {
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMercaderia", x => x.TipoMercaderiaId);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.ComandaId);
                    table.ForeignKey(
                        name: "FK_Comanda_FormaEntrega_FormaEntregaId",
                        column: x => x.FormaEntregaId,
                        principalTable: "FormaEntrega",
                        principalColumn: "FormaEntregaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mercaderia",
                columns: table => new
                {
                    MercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Ingredientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Preparacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercaderia", x => x.MercaderiaId);
                    table.ForeignKey(
                        name: "FK_Mercaderia_TipoMercaderia_TipoMercaderiaId",
                        column: x => x.TipoMercaderiaId,
                        principalTable: "TipoMercaderia",
                        principalColumn: "TipoMercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComandaMercaderia",
                columns: table => new
                {
                    ComandaMercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MercaderiaId = table.Column<int>(type: "int", nullable: false),
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaMercaderia", x => x.ComandaMercaderiaId);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comanda",
                        principalColumn: "ComandaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Mercaderia_MercaderiaId",
                        column: x => x.MercaderiaId,
                        principalTable: "Mercaderia",
                        principalColumn: "MercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FormaEntrega",
                columns: new[] { "FormaEntregaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Salon" },
                    { 2, "Delivery" },
                    { 3, "Pedidos Ya" }
                });

            migrationBuilder.InsertData(
                table: "TipoMercaderia",
                columns: new[] { "TipoMercaderiaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Entrada" },
                    { 2, "Minuta" },
                    { 3, "Pastas" },
                    { 4, "Parrilla" },
                    { 5, "Pizzas" },
                    { 6, "Sandwich" },
                    { 7, "Ensalada" },
                    { 8, "Bebidas" },
                    { 9, "Cerveza Artesanal" },
                    { 10, "Postres" }
                });

            migrationBuilder.InsertData(
                table: "Mercaderia",
                columns: new[] { "MercaderiaId", "Imagen", "Ingredientes", "Nombre", "Precio", "Preparacion", "TipoMercaderiaId" },
                values: new object[,]
                {
                    { 1, "https://drive.google.com/uc?export=download&id=14T7uOtb9yIaukf7EvPw99ntbb4A5M2q8", "bastones de queso (6)", "deditos de queso", 600, "empanizar 6 bastones de queso y freirlos en abundante aceite", 1 },
                    { 2, "https://drive.google.com/uc?export=download&id=1tTY1nVA9cxIm4NyRcadOC1sQQMAN577w", "papas (1), huevo (2), queso (100gr), jamon(100gr)", "tortilla de papa", 1200, "formar una tortilla de forma redonda y plana con huevo y trozos de papas, rellno de jamon y queso", 2 },
                    { 3, "https://drive.google.com/uc?export=download&id=1JBK2cdZo-jimrJRYLYmMrvQQguh7PapL", "crema (30gr), ricota (200gr)", "sorrentinos de ricota", 1500, "pasta rellena en forma redonda con ricota y crema", 3 },
                    { 4, "https://drive.google.com/uc?export=download&id=11VXVqMDgXd802UwA-u3PFtD-4MZDu5UA", "entrañas (1), limon (1), perejil fresco (50gr)", "entraña con papas fritas", 2600, "cocinar la entraña aderezada con limon y perejil en parrilla bien caliente con mucha brasa, freir las papas en aceite", 4 },
                    { 5, "https://drive.google.com/uc?export=download&id=1c4XjtSEudmSoPSzAbXoTatNJ_RJ_lYe6", "salsa de tomate (100cc), albahaca (7), mozzarella (500gr)", "pizza", 1300, "untar salsa de tomate a la masa, agregar la mozzarella y cocinar hasta grtinar el queso, añadir las hojas de albahaca", 5 },
                    { 6, "https://drive.google.com/uc?export=download&id=1dR8NBAgnHhdwu1FrjO8uLUA3dqjbsZ_z", "rebanadas de pan (4), fetas de queso (2), filetes de pollo (2), lechuga (2hojas), tomate (4rebanadas), mayonesa (30gr)", "sandwich club", 800, "tostar el pan, aderezarlo con mayonesa, colocar las fetas de queso, el tomate, los filetes de pollo y la lechuga", 6 },
                    { 7, "https://drive.google.com/uc?export=download&id=12wd13076D1W6sYiPPNzpRU-rr_44WQXj", "tomate cherry rojo (100gr), tomate cherry amarillo (100gr), tomate pera (1), cebolla morada (1), diente de ajo (1), aceite de oliva (45ml), vinagre (20ml)", "panzanella italiana", 600, "cortar el tomate cherry rojo y amarillo por la mitad, cortar en rodajas el tomate pera y cortar la cebolla en juliana, picar el ajo y aderezar con vinagre y aceite de oliva", 7 },
                    { 8, "https://drive.google.com/uc?export=download&id=1Gx6OtOL24pGD2eUbd0vdlBrX8hVEcRyG", "azúcar (1cda), jugo de lima (30ml), menta fresca (5), ron blanco (60ml), cubos de hielo (4), soda (120ml)", "mojito", 300, "en un vaso, mezclar las hojas de menta con el jugo de lima y el azúcar. Agregar el ron junto con los cubos de hielo. Luego, echar la soda y revolver", 8 },
                    { 9, "https://drive.google.com/uc?export=download&id=1mFg7nExEcLvb-TLd-CCXGtAqaE_B_ZQ4", "cerveza rubia (800cc), miel (2cdas), almíbar de frutas (2cdas)", "cerveza a la miel", 600, "en un vaso verter la cerveza rubia, colocar el almibar de frutas,la miel y revolver", 9 },
                    { 10, "https://drive.google.com/uc?export=download&id=1hv4T7tOZjNO7QFod12BYqBbrHRFEwPxi", "leche (60ml), nata líquida (40ml), maizena (10gr), azúcar (10gr), mini galletas con pepitas de chocolate (3)", "crema de vainilla con cookies", 400, "en una cacerola calentar la leche, el azucar y la nata con la maicena hasta espesar, dejar enfriar en una compotera y servir con las mini cookies", 10 },
                    { 11, "https://drive.google.com/uc?export=download&id=1UNuJM9HQ1G06nC_O8Ewr_ccWBB-Rx2SE", "parmesano rallado (100gr), mozzarella rallada (200gr),Tomates cherry cortados en cuartos (200gr), albahaca (4hojas)", "empanadas caprese", 400, "cortamos la albahaca y la incorporamos en un bol junto a la mozzarella, el parmesano y los tomates cherry cortados en cuartos, salpimentamos, rellenamos las tapitas de empanadas y cocinamos", 1 },
                    { 12, "https://drive.google.com/uc?export=download&id=1ZHtIgaEBYu8yh_4JlSVtfzVgGua489q2", "calabaza mediana (1), queso rallado (100gr), hierbas secas", "canastatitas de calabaza", 500, "hervir la calabaza para hacer un pure, mezclarla con queso rallado y las hierbas, formar canastas con tapitas de empanadas, rellenarlas y cocinar", 2 },
                    { 13, "https://drive.google.com/uc?export=download&id=1u3ymdD7Yk8cP3LsRk39cl9i3wd13ch7Z", "ricota (500gr), huevo (1), harina (225gr)", "ñoquis de ricota", 1200, "colocar en un bol la ricota y el huevo, salpimentar, mezclar e integrar la harina, formar la masa y cortarla en cubitos y cocinarlos en agua hirviendo hasta que floten", 3 },
                    { 14, "https://drive.google.com/uc?export=download&id=1pJNiFNZLp7B_vTCex_4Cslgw8_nFk-KG", "matambre (1), leche (1000ml), ajo (1), laurel (4)", "matambre a la parrilla", 3000, "macerar el matambre durante una noche cubierto de leche, las hojas de laurel y el ajo, llevar a una parrilla a fuego medio del lado de la grasa hasta que esté dorado, dar vuelta y dorar del lado restante", 4 },
                    { 15, "https://drive.google.com/uc?export=download&id=1tph3tQXtdVtNjcIaaNXJMTawKemEvf1C", "anana (4rodajas), queso rallado (200gr), jamon cocido (400gr), queso mozzarella (300gr), salsa de tomate (250ml)", "pizza hawaiana", 1300, "cortar en trozos el jamón y en cubos el anana, untamos la masa con salsa de tomate, cubrir con el queso rallado, repartir por encima el jamón cocido y el anana y cocinar", 5 },
                    { 16, "https://drive.google.com/uc?export=download&id=1sgRyeSS2z8dr39_3ZPeRCRRTkfISfFqz", "rebanadas de pan (4), jamon cocido (2fetas), salami (2fetas), lomo de cerdo asado (2lonchas), mostaza (10gr) ", "sandwich cubano", 700, "cortamos los panes por la mitad, untarlos con mostaza y colocar el jamón, el salami, el lomo, el queso", 6 },
                    { 17, "https://drive.google.com/uc?export=download&id=1FtxE8-4g7XZ5Ltju1RqcNXFiE5g4QbqX", "lechuga (1), cebolla morada (1), zanahoria (1), tomates cherrys rojos (4)", "ensalada multicolor", 500, "cortar la lechuga y la cebolla morada en tiras, rallar la zanahoria, cortar los tomates por la mitad, aderezar y salpimentar", 7 },
                    { 18, "https://drive.google.com/uc?export=download&id=1l2ywuMdj-xuVOc-QOJFClgOWRxIPu4Xy", "melon (1/2), agua fria (1000ml), azucar (20gr), hielo (7)", "jugo de melon", 300, "en una licuadora agregar el melon cortado en cubos, el agua y el azucar, licuar hasta que quede homgeneo, servir en una jarra con los hielos", 8 },
                    { 19, "https://drive.google.com/uc?export=download&id=18svxxGvBjCxuehiTiMwp9aqhTXe_7BQ5", "cerveza rubia (1 lata), gin (1 medida), jugo de limon (1 medida)", "cerveza con gin y limon", 900, "en un vaso verter la cerveza rubia, el gin y el jugo de limon y revolver", 9 },
                    { 20, "https://drive.google.com/uc?export=download&id=1RtTHSy7oWWv3pYMGib8RS0FhKvFpczz_", "leche (60ml), claras de huevo (50ml), sobre de gelatina sin sabor (1), azúcar (10gr)", "crema de leche merengada", 500, "hervir la leche con el azúcar, disolvemos la gelatina en la mezcla, batir las claras a punto de nieve con un poco de azúcar, incorporar las claras y batir hasta que se integre todo y dejar enfriar en la heladera", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_FormaEntregaId",
                table: "Comanda",
                column: "FormaEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_ComandaId",
                table: "ComandaMercaderia",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_MercaderiaId",
                table: "ComandaMercaderia",
                column: "MercaderiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mercaderia_TipoMercaderiaId",
                table: "Mercaderia",
                column: "TipoMercaderiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComandaMercaderia");

            migrationBuilder.DropTable(
                name: "Comanda");

            migrationBuilder.DropTable(
                name: "Mercaderia");

            migrationBuilder.DropTable(
                name: "FormaEntrega");

            migrationBuilder.DropTable(
                name: "TipoMercaderia");
        }
    }
}
