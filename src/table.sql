create table contents
(
	content_id int identity,
	content_name varchar(256),
	content_fields varchar(256)
)
go

create unique index table_name_content_id_uindex
	on contents (content_id)
go

create unique index contents_content_name_uindex
	on contents (content_name)
go

