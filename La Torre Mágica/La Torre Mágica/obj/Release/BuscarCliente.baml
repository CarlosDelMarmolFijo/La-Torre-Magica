   M S B A M L   `   `   `  ����   La Torre Mágica$    La_Torre_Mágica.BuscarCliente   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  Buscar Cliente��$	��420��$	��500�� � WindowStartupLocation$ CenterScreen=� � Icon$ images/menu/buscar.ico�� � 
ResizeMode$ NoResize=�����  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.���  � Color$ 	LightBlue�� � Offset$	 0.0Q�� $ White��$	 1.0Q�.��� $��20q� � ColumnDefinitions �� $
��Auto ��� $��* �	 � RowDefinitions	 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $"��Indique el cliente a buscar:��$��Center=�$��	0,10,10,0q�$��0��$��0��$��Right=�$��30��$
��Bold"��� -   $��TextBoxNombreCliente��$��1��$��0��$��5q�$��30��$��Center=�$
��Left=�$��Center=�
 �� TextAlignment$

 Left=��� $��1��$��1�� �� Orientation$ 
Horizontal=�$��Right=�.+��� -   $��ButtonAceptar��$��Aceptar��$��80��$��5q�$��30�����#4CAF50
�������
������ $��1q�$��14(��� -   $��ButtonCancelar��$��Cancelar��$��80��$��5q�$��30�����#f44336
�������
������ $��1q�$��14(��� $��Información del Cliente:��$��Center=�$��	0,20,10,0q�$��0��$��2��$��Right=�$��30��$
��Bold"��� -   $��TextBoxInformacionCliente��$��1��$��2��$��5q�$	��250��" �� VerticalScrollBarVisibility$
 Auto=� �� 
IsReadOnly .  �� AcceptsReturn . $
 Center=� �� TextWrapping$
 Wrap=�