Drop database CompteBancaire;
Create database CompteBancaire;
use CompteBancaire;

create table CompteBancaire(
	Id int identity primary key,
	TypeCompte varchar(50) 
)

create table Transactions(
	Id int identity primary key,
	TransactionType varchar(50),
	DateTransaction datetime default getdate(),
	Montant decimal,
	CompteBancaireId int

	constraint FK_compteBancaireId foreign key (CompteBancaireId) references comptebancaire(Id)
)

select * from CompteBancaire;
select * from Transactions;