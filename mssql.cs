# Tips & Tricks

# Date

getdate() 			// current datetime
MONTH(getdate()) 	// month number


# MSSQL - SMALLDATETIME

In MSSQL table: 2020-05-21 15:33:00 | 15:33:00.000

// output: May 21 2020 03:33:00:000PM
mysql: SELECT [Date]											

// output: 21.05.2020 15:33:00
mysql: (CONVERT(varchar, [Date], 104) + ' ' + CONVERT(varchar, [Date], 108))

# Create Table with PK

CREATE TABLE [table] (
    id int NOT NULL,
    name varchar(255) NOT NULL, 			// no-default
	points float NOT NULL DEFAULT ((0.00)),	// is-default
    PRIMARY KEY (id)
);

# Alter Table

ALTER TABLE [table] ADD [column] int NOT NULL DEFAULT((0))
