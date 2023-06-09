PGDMP                         {         
   SBPMotorDB    10.23    10.23 '               0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false                       1262    16431 
   SBPMotorDB    DATABASE     �   CREATE DATABASE "SBPMotorDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Spanish_Spain.1252' LC_CTYPE = 'Spanish_Spain.1252';
    DROP DATABASE "SBPMotorDB";
             postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
             postgres    false                       0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                  postgres    false    3                        3079    12924    plpgsql 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;
    DROP EXTENSION plpgsql;
                  false                       0    0    EXTENSION plpgsql    COMMENT     @   COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';
                       false    1            �            1259    16624    catalogo    TABLE     b   CREATE TABLE public.catalogo (
    id integer NOT NULL,
    descripcion character varying(255)
);
    DROP TABLE public.catalogo;
       public         postgres    false    3            �            1259    16622    catalogo_id_seq    SEQUENCE     �   CREATE SEQUENCE public.catalogo_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.catalogo_id_seq;
       public       postgres    false    3    199                       0    0    catalogo_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.catalogo_id_seq OWNED BY public.catalogo.id;
            public       postgres    false    198            �            1259    16650    tarjeta    TABLE       CREATE TABLE public.tarjeta (
    id integer NOT NULL,
    fecha_ingreso timestamp without time zone NOT NULL,
    fecha_salida timestamp without time zone,
    vehiculo_id integer NOT NULL,
    costo numeric,
    plaza numeric,
    estado boolean,
    aplicadescuento boolean
);
    DROP TABLE public.tarjeta;
       public         postgres    false    3            �            1259    16648    tarjeta_id_seq    SEQUENCE     �   CREATE SEQUENCE public.tarjeta_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.tarjeta_id_seq;
       public       postgres    false    203    3                       0    0    tarjeta_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.tarjeta_id_seq OWNED BY public.tarjeta.id;
            public       postgres    false    202            �            1259    16616    tipo_vehiculo    TABLE     g   CREATE TABLE public.tipo_vehiculo (
    id integer NOT NULL,
    descripcion character varying(255)
);
 !   DROP TABLE public.tipo_vehiculo;
       public         postgres    false    3            �            1259    16614    tipo_vehiculo_id_seq    SEQUENCE     �   CREATE SEQUENCE public.tipo_vehiculo_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE public.tipo_vehiculo_id_seq;
       public       postgres    false    197    3                       0    0    tipo_vehiculo_id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public.tipo_vehiculo_id_seq OWNED BY public.tipo_vehiculo.id;
            public       postgres    false    196            �            1259    16632    vehiculo    TABLE     �   CREATE TABLE public.vehiculo (
    id integer NOT NULL,
    placa character varying(50) NOT NULL,
    tipo_vehiculo_id integer NOT NULL,
    catalogo_id integer NOT NULL
);
    DROP TABLE public.vehiculo;
       public         postgres    false    3            �            1259    16630    vehiculo_id_seq    SEQUENCE     �   CREATE SEQUENCE public.vehiculo_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.vehiculo_id_seq;
       public       postgres    false    3    201                       0    0    vehiculo_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.vehiculo_id_seq OWNED BY public.vehiculo.id;
            public       postgres    false    200            �
           2604    16627    catalogo id    DEFAULT     j   ALTER TABLE ONLY public.catalogo ALTER COLUMN id SET DEFAULT nextval('public.catalogo_id_seq'::regclass);
 :   ALTER TABLE public.catalogo ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    198    199    199            �
           2604    16653 
   tarjeta id    DEFAULT     h   ALTER TABLE ONLY public.tarjeta ALTER COLUMN id SET DEFAULT nextval('public.tarjeta_id_seq'::regclass);
 9   ALTER TABLE public.tarjeta ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    203    202    203            �
           2604    16619    tipo_vehiculo id    DEFAULT     t   ALTER TABLE ONLY public.tipo_vehiculo ALTER COLUMN id SET DEFAULT nextval('public.tipo_vehiculo_id_seq'::regclass);
 ?   ALTER TABLE public.tipo_vehiculo ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    197    196    197            �
           2604    16635    vehiculo id    DEFAULT     j   ALTER TABLE ONLY public.vehiculo ALTER COLUMN id SET DEFAULT nextval('public.vehiculo_id_seq'::regclass);
 :   ALTER TABLE public.vehiculo ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    200    201    201                      0    16624    catalogo 
   TABLE DATA               3   COPY public.catalogo (id, descripcion) FROM stdin;
    public       postgres    false    199   �(                 0    16650    tarjeta 
   TABLE DATA               v   COPY public.tarjeta (id, fecha_ingreso, fecha_salida, vehiculo_id, costo, plaza, estado, aplicadescuento) FROM stdin;
    public       postgres    false    203   �(       
          0    16616    tipo_vehiculo 
   TABLE DATA               8   COPY public.tipo_vehiculo (id, descripcion) FROM stdin;
    public       postgres    false    197   3)                 0    16632    vehiculo 
   TABLE DATA               L   COPY public.vehiculo (id, placa, tipo_vehiculo_id, catalogo_id) FROM stdin;
    public       postgres    false    201   p)                  0    0    catalogo_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.catalogo_id_seq', 2, true);
            public       postgres    false    198                       0    0    tarjeta_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.tarjeta_id_seq', 14, true);
            public       postgres    false    202                       0    0    tipo_vehiculo_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.tipo_vehiculo_id_seq', 2, true);
            public       postgres    false    196                        0    0    vehiculo_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.vehiculo_id_seq', 16, true);
            public       postgres    false    200            �
           2606    16629    catalogo catalogo_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.catalogo
    ADD CONSTRAINT catalogo_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.catalogo DROP CONSTRAINT catalogo_pkey;
       public         postgres    false    199            �
           2606    16655    tarjeta tarjeta_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.tarjeta
    ADD CONSTRAINT tarjeta_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.tarjeta DROP CONSTRAINT tarjeta_pkey;
       public         postgres    false    203            �
           2606    16621     tipo_vehiculo tipo_vehiculo_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.tipo_vehiculo
    ADD CONSTRAINT tipo_vehiculo_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.tipo_vehiculo DROP CONSTRAINT tipo_vehiculo_pkey;
       public         postgres    false    197            �
           2606    16637    vehiculo vehiculo_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.vehiculo
    ADD CONSTRAINT vehiculo_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.vehiculo DROP CONSTRAINT vehiculo_pkey;
       public         postgres    false    201            �
           2606    16656     tarjeta tarjeta_vehiculo_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.tarjeta
    ADD CONSTRAINT tarjeta_vehiculo_id_fkey FOREIGN KEY (vehiculo_id) REFERENCES public.vehiculo(id);
 J   ALTER TABLE ONLY public.tarjeta DROP CONSTRAINT tarjeta_vehiculo_id_fkey;
       public       postgres    false    2698    201    203            �
           2606    16643 "   vehiculo vehiculo_catalogo_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.vehiculo
    ADD CONSTRAINT vehiculo_catalogo_id_fkey FOREIGN KEY (catalogo_id) REFERENCES public.catalogo(id);
 L   ALTER TABLE ONLY public.vehiculo DROP CONSTRAINT vehiculo_catalogo_id_fkey;
       public       postgres    false    201    199    2696            �
           2606    16638 '   vehiculo vehiculo_tipo_vehiculo_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.vehiculo
    ADD CONSTRAINT vehiculo_tipo_vehiculo_id_fkey FOREIGN KEY (tipo_vehiculo_id) REFERENCES public.tipo_vehiculo(id);
 Q   ALTER TABLE ONLY public.vehiculo DROP CONSTRAINT vehiculo_tipo_vehiculo_id_fkey;
       public       postgres    false    197    201    2694               #   x�3�t�IM.)�L��2���L*�L������ sN�         R   x��ͱ�0D�ڞ��%Ɖ35d$
$�_��*�&쉾!F� ލx]h�3ܣ�n�"�:䔩�����ǡ��YU/(��      
   -   x�3���/�O�L�I-I�2�K��L.��W��LO-������ �5g         N   x�3�,.)��K�4�4�2C�#s,�9F��F@�&�X"�04@Hq���PT��LQT�qz����-����� �)�     