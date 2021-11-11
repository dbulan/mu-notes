# Tips & Tricks

# Date

getdate() 			// current datetime
MONTH(getdate()) 	// month number

# Create Table with PK

CREATE TABLE [table] (
    id int NOT NULL,
    name varchar(255) NOT NULL, 			// no-default
	points float NOT NULL DEFAULT ((0.00)),	// is-default
    PRIMARY KEY (id)
);

# Alter Table

ALTER TABLE [table] ADD [column] int NOT NULL DEFAULT((0))
