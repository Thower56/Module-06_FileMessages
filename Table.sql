Create database CompteBancaire;

use CompteBancaire;

create table CompteBancaire(
	Id UniqueIdentifier primary key,
	TypeCompte varchar(50) 
)

create table Transactions(
	Id UniqueIdentifier primary key,
	TransactionType varchar(50),
	DateTransaction datetime default getdate(),
	Montant decimal,
	CompteBancaireId UniqueIdentifier

	constraint FK_compteBancaireId foreign key (CompteBancaireId) references comptebancaire(Id)
)