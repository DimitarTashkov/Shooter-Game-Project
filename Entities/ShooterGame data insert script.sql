INSERT INTO Weapons([Name],AmmoType,[Power],Damage,HeadShot)
VALUES
('Rifle',22,20,0,0)
,('Shotgun',9,33,0,0)
,('Sniper',1,800,0,0)

INSERT INTO Maps (X,Y,Terrain)
VALUES
(5,5,'5:5')

INSERT INTO Enemies([Name],Size,Health,Life,IsRegenerated)
VALUES
('Orc',15,40,0,0)
,('Tank',50,80,0,0)
,('Warrior',30,30,0,0)
,('Wizard',25,50,0,0)

UPDATE Weapons
SET Damage = AmmoType*[Power]
WHERE Id IN (1,2,3)

UPDATE Enemies
SET Life = Size*Health
WHERE Id IN (1,2,3,4)

--some selects for validation
SELECT * FROM Weapons

SELECT * FROM Enemies

SELECT * FROM Maps