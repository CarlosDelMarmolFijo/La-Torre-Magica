   M S B A M L   `   `   `  ����  La Torre Mágica$    La_Torre_Mágica.BuscarCliente   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Buscar Cliente��5   	   $	��420��5   	   $	��500��5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � Icon$ images/menu/buscar.ico��5   	    � 
ResizeMode$ NoResize=�5   	   ��5      �� 5   
    �� 
StartPoint$	 0,0&�6    �� EndPoint$	 1,1&�6/   .��5      �  � Color$ 	LightBlue��6    � Offset$	 0.0Q�6-   � 5      $ White��6   $	 1.0Q�6)   5      .��5      � $��20q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   	 � RowDefinitions	 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $��* �6   .+�5$   
   �� $"��Indique el cliente a buscar:��6   $��Center=�68   $��	0,10,10,0q�6S   $��0��6f   $��0��6v   $��Right=�6�   $��30��6�   $
��Bold"�6�   �� 5%   
   -   5        $��TextBoxNombreCliente��5%      $��1��6.   $��0��6>   $��5q�6K   $��30��6V   $��Center=�6b   $
��Left=�6}   $��Center=�6�   
 �� TextAlignment$

 Left=�6�   �� 5(   
   $��1��6   $��1��6%    �� Orientation$ 
Horizontal=�62   $��Right=�6K   .+�5)      �� -   5        $��ButtonAceptar��5)      $��Aceptar��5*      $��80��6'   $��5q�62   $��30��6=   ���#4CAF50
�������
������ $��1q�5,   W   $��14(�6k   �� 5-      -   5        $��ButtonCancelar��5-      $��Cancelar��5.      $��80��6(   $��5q�63   $��30��6>   ���#f44336
�������
������ $��1q�50   W   $��14(�6k   51      �� 54   
   $��Información del Cliente:��6   $��Center=�64   $��	0,20,10,0q�6O   $��0��6b   $��2��6r   $��Right=�6   $��30��6�   $
��Bold"�6�   �� 55   
   -   5        $��TextBoxInformacionCliente��55      $��1��56      $��2��57      $��5q�58      $	��250��59      " �� VerticalScrollBarVisibility$
 Auto=�5:       �� 
IsReadOnly .  �� AcceptsReturn . $
 Center=�5=       �� TextWrapping$
 Wrap=�5>      5?      5@      