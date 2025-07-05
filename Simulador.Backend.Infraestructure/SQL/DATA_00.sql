INSERT INTO Rol (Id, Name, Description, CreatedAt, CreatedBy)
VALUES 
(1, 'Admin', 'Administrador del sistema', GETDATE(), 'system'),
(2, 'Student', 'Usuario estudiante', GETDATE(), 'system');


INSERT INTO Entity (Name, Description, CreatedAt, CreatedBy)
VALUES 
('Simulador', 'Entidad principal del sistema', GETDATE(), 'system');


INSERT INTO Course (Name, Description, EntityId, QuestionCount, TimeMinute, CreatedAt, CreatedBy)
VALUES 
('Curso PMP', 'Curso de preparaci�n para PMP', 1, 100, 120, GETDATE(), 'admin');


INSERT INTO Topic (Name, Description, CourseId, CreatedAt, CreatedBy)
VALUES 
('Inicio del proyecto', 'Primera fase del proyecto', 1, GETDATE(), 'admin');


INSERT INTO Issue (Name, Description, TopicId, CreatedAt, CreatedBy)
VALUES 
('Acta de constituci�n', 'Documento clave del inicio del proyecto', 1, GETDATE(), 'admin');

INSERT INTO StatusSubscription (Id, Name, Description, CreatedAt, CreatedBy)
VALUES 
(1, 'Activa', 'Suscripci�n en estado activo', GETDATE(), 'system'),
(2, 'Inactiva', 'Suscripci�n expirada o cancelada', GETDATE(), 'system');

INSERT INTO ExamMode (Id, Name, Description, CreatedAt, CreatedBy)
VALUES 
(1, 'Pr�ctica', 'Modo de pr�ctica sin tiempo l�mite', GETDATE(), 'admin'),
(2, 'Simulaci�n', 'Simulaci�n del examen real con tiempo l�mite', GETDATE(), 'admin');

INSERT INTO TypeExam (Id, Name, Description, CreatedAt, CreatedBy)
VALUES 
(1, 'Evaluaci�n Final', 'Examen final del curso', GETDATE(), 'admin'),
(2, 'Autoevaluaci�n', 'Examen de autoevaluaci�n al finalizar el tema', GETDATE(), 'admin');


INSERT INTO Subscription (Name, Description, ExpirationDate, SubscriptionDate, CourseId, StatusId, UserId, CreatedAt, CreatedBy)
VALUES 
('Suscripci�n PMP B�sico', 'Acceso al curso PMP', '2025-12-31', GETDATE(), 1, 1, '3fa85f64-5717-4562-b3fc-2c963f66afa6', GETDATE(), 'admin');
