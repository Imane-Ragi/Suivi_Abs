using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Suivi_Abs.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comptes",
                columns: table => new
                {
                    id_C = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(nullable: false),
                    password = table.Column<string>(maxLength: 10, nullable: false),
                    matricule = table.Column<string>(nullable: false),
                    nComplet = table.Column<string>(nullable: true),
                    poste = table.Column<string>(nullable: true),
                    roleC = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comptes", x => x.id_C);
                });

            migrationBuilder.CreateTable(
                name: "filieres",
                columns: table => new
                {
                    id_F = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nFiliere = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filieres", x => x.id_F);
                });

            migrationBuilder.CreateTable(
                name: "professeurs",
                columns: table => new
                {
                    id_P = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    matricule = table.Column<string>(nullable: false),
                    nComplet = table.Column<string>(nullable: true),
                    specialité = table.Column<string>(nullable: true),
                    dateN = table.Column<DateTime>(nullable: false),
                    dateR = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professeurs", x => x.id_P);
                });

            migrationBuilder.CreateTable(
                name: "groupes",
                columns: table => new
                {
                    id_G = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Numgroupe = table.Column<string>(nullable: true),
                    id_F = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groupes", x => x.id_G);
                    table.ForeignKey(
                        name: "FK_groupes_filieres_id_F",
                        column: x => x.id_F,
                        principalTable: "filieres",
                        principalColumn: "id_F",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matieres",
                columns: table => new
                {
                    id_M = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nMatiere = table.Column<string>(nullable: true),
                    id_P = table.Column<int>(nullable: false),
                    id_F = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matieres", x => x.id_M);
                    table.ForeignKey(
                        name: "FK_matieres_filieres_id_F",
                        column: x => x.id_F,
                        principalTable: "filieres",
                        principalColumn: "id_F",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matieres_professeurs_id_P",
                        column: x => x.id_P,
                        principalTable: "professeurs",
                        principalColumn: "id_P",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "etudiants",
                columns: table => new
                {
                    id_E = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    matricule = table.Column<string>(nullable: false),
                    Ncomplet = table.Column<string>(nullable: true),
                    dateN = table.Column<DateTime>(nullable: false),
                    lieu_n = table.Column<string>(nullable: true),
                    adresse = table.Column<string>(nullable: true),
                    imageE = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    id_G = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etudiants", x => x.id_E);
                    table.ForeignKey(
                        name: "FK_etudiants_groupes_id_G",
                        column: x => x.id_G,
                        principalTable: "groupes",
                        principalColumn: "id_G",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "seances",
                columns: table => new
                {
                    id_S = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date_S = table.Column<DateTime>(nullable: false),
                    Num_S = table.Column<string>(nullable: true),
                    Duree = table.Column<string>(nullable: true),
                    numSalle = table.Column<string>(nullable: true),
                    id_M = table.Column<int>(nullable: false),
                    id_G = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seances", x => x.id_S);
                    table.ForeignKey(
                        name: "FK_seances_groupes_id_G",
                        column: x => x.id_G,
                        principalTable: "groupes",
                        principalColumn: "id_G",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_seances_matieres_id_M",
                        column: x => x.id_M,
                        principalTable: "matieres",
                        principalColumn: "id_M",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "absences",
                columns: table => new
                {
                    id_RES = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Statut = table.Column<string>(nullable: true),
                    id_S = table.Column<int>(nullable: false),
                    id_E = table.Column<int>(nullable: false),
                    id_G = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_absences", x => x.id_RES);
                    table.ForeignKey(
                        name: "FK_absences_etudiants_id_E",
                        column: x => x.id_E,
                        principalTable: "etudiants",
                        principalColumn: "id_E",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_absences_groupes_id_G",
                        column: x => x.id_G,
                        principalTable: "groupes",
                        principalColumn: "id_G",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_absences_seances_id_S",
                        column: x => x.id_S,
                        principalTable: "seances",
                        principalColumn: "id_S",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_absences_id_E",
                table: "absences",
                column: "id_E");

            migrationBuilder.CreateIndex(
                name: "IX_absences_id_G",
                table: "absences",
                column: "id_G");

            migrationBuilder.CreateIndex(
                name: "IX_absences_id_S",
                table: "absences",
                column: "id_S");

            migrationBuilder.CreateIndex(
                name: "IX_etudiants_id_G",
                table: "etudiants",
                column: "id_G");

            migrationBuilder.CreateIndex(
                name: "IX_groupes_id_F",
                table: "groupes",
                column: "id_F");

            migrationBuilder.CreateIndex(
                name: "IX_matieres_id_F",
                table: "matieres",
                column: "id_F");

            migrationBuilder.CreateIndex(
                name: "IX_matieres_id_P",
                table: "matieres",
                column: "id_P");

            migrationBuilder.CreateIndex(
                name: "IX_seances_id_G",
                table: "seances",
                column: "id_G");

            migrationBuilder.CreateIndex(
                name: "IX_seances_id_M",
                table: "seances",
                column: "id_M");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "absences");

            migrationBuilder.DropTable(
                name: "comptes");

            migrationBuilder.DropTable(
                name: "etudiants");

            migrationBuilder.DropTable(
                name: "seances");

            migrationBuilder.DropTable(
                name: "groupes");

            migrationBuilder.DropTable(
                name: "matieres");

            migrationBuilder.DropTable(
                name: "filieres");

            migrationBuilder.DropTable(
                name: "professeurs");
        }
    }
}
