
SELECT
(
	SELECT				p.BusinessEntityID AS '@id',
	(
		SELECT				p.Title AS '@title',
							p.FirstName AS '@first',
							p.MiddleName AS '@middle',
							p.LastName AS '@last',
							p.Suffix AS '@suffix'
		FOR					XML PATH('name'), TYPE
	),
	(
		SELECT				a.AddressLine1 AS '@addr1',
							a.AddressLine2 AS '@addr2',
							a.City AS '@city',
							sp.Name AS '@stateProv',
							cr.Name AS '@country',
							a.PostalCode AS '@postal'
		FROM				Person.BusinessEntityAddress AS bea
		LEFT OUTER JOIN		Person.Address AS a ON a.AddressID = bea.AddressID
		INNER JOIN			Person.StateProvince AS sp ON sp.StateProvinceID = a.StateProvinceID
		INNER JOIN			Person.CountryRegion AS cr ON cr.CountryRegionCode = sp.CountryRegionCode
		WHERE				bea.BusinessEntityID = p.BusinessEntityID
		FOR					XML PATH('address'), TYPE
	),
	(
		SELECT				pp.PhoneNumber AS '@num',
							pnt.Name AS '@type'
		FROM				Person.PersonPhone AS pp
		LEFT OUTER JOIN		Person.PhoneNumberType AS pnt ON pp.PhoneNumberTypeID = pnt.PhoneNumberTypeID
		WHERE				pp.BusinessEntityID = p.BusinessEntityID
		FOR					XML PATH('phone'), TYPE
	),
	(
		SELECT				ea.EmailAddress AS '@addr'
		FROM				Person.EmailAddress AS ea 
		WHERE				ea.BusinessEntityID = p.BusinessEntityID
		FOR					XML PATH('email'), TYPE
	)

	FROM				Person.Person AS p
	FOR					XML PATH('person'), TYPE
)
FOR					XML PATH('people')
