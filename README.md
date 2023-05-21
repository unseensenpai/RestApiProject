# Generate database and run the project.

**-- Create database named TestDbForTestProject**

```
CREATE DATABASE TestDbForTestProject;
```

**-- Generate connection string for TestDbForTestProject**
```
USE TestDbForTestProject;
GO
```


**-- Add employee table with the specified columns**
```
CREATE TABLE Employee
(
  id INT NOT NULL IDENTITY(1,1),
  name VARCHAR(255) NOT NULL,
  surname VARCHAR(255) NOT NULL,
  age INT NOT NULL,
  startdate DATE NOT NULL,
  enddate DATE NULL,
  identityno VARCHAR(255) NOT NULL,
  salary MONEY NOT NULL
);
```

**-- Generate 100 employee samples**
```
INSERT INTO Employee (name, surname, age, startdate, enddate, identityno, salary)
SELECT TOP (100)
  'John' + RIGHT('0000' + CONVERT(VARCHAR(5), ROW_NUMBER() OVER (ORDER BY @@SPID)), 5),
  'Doe' + RIGHT('0000' + CONVERT(VARCHAR(5), ROW_NUMBER() OVER (ORDER BY @@SPID)), 5),
  RAND(CHECKSUM(NEWID())) * 100,
  GETDATE(),
  DATEADD(DAY, RAND(CHECKSUM(NEWID())) * 365, GETDATE()),
  '123456789' + RIGHT('0000' + CONVERT(VARCHAR(5), ROW_NUMBER() OVER (ORDER BY @@SPID)), 5),
  RAND(CHECKSUM(NEWID())) * 10000
FROM master..spt_values;
```