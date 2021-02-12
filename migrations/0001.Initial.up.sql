CREATE TABLE public.vehicle_category (
    id bigint DEFAULT next_id() NOT NULL,
    short_name text,
    description text NOT NULL,
    created_at timestamp with time zone DEFAULT now() NOT NULL,
    CONSTRAINT vehicle_category_pkey PRIMARY KEY (id)

);


CREATE TABLE public.vehicle_type (
    id bigint DEFAULT next_id() NOT NULL,
    vehicle_category_id bigint NOT NULL,
    short_name text,
    description text NOT NULL,
    created_at timestamp with time zone DEFAULT now() NOT NULL,
    CONSTRAINT vehicle_type_pkey PRIMARY KEY (id),
    CONSTRAINT vehicle_type_vehicle_category_fkey FOREIGN KEY (vehicle_category_id)
        REFERENCES public.vehicle_category (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION

);



CREATE TABLE public.vehicle_tax (
    id bigint DEFAULT next_id() NOT NULL,
    vehicle_type_id bigint NOT NULL,
    import_duty numeric(7,4),
    vat numeric(7,4),
    nhil numeric(7,4),
    getfund_levy numeric(7,4),
    au_levy numeric(7,4),
    ecowas_levy numeric(7,4),
    exim_levy numeric(7,4),
    exam_levy numeric(7,4),
    processing_fee numeric(7,4),
    special_import_levy numeric(7,4),
    created_at timestamp with time zone DEFAULT now() NOT NULL,
    CONSTRAINT vehicle_tax_pkey PRIMARY KEY (id),
    CONSTRAINT vehicle_tax_vehicle_type_key UNIQUE (vehicle_type_id),
    CONSTRAINT vehicle_tax_vehicle_type_fkey FOREIGN KEY (vehicle_type_id)
        REFERENCES public.vehicle_type (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION

);
