   M S B A M L   `   `   `  ����	  Sklep    
Sklep.Edit   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Aav2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6C     � Title$  	LoginView��5      $	��550��6   $	��556��6$    � WindowStyle$
 None=�5       � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterScreen=�5      
������  � AllowsTransparency$
 True��5
      .��5      ��  �� CornerRadius$ 12��6   ._�5   
   �� $ 10��6   $��2q�5       J� Opacity$
 0.95Q�5      $��0,10,0,0q�6    ���	#FF131725��5      �� 5       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5      � 	 � Color$	 White��6#   
 � Offset$
 0Q�61   � 5      $	 	#FF43A073��6#   $

 0.75Q�65   � 5      $	 	#FF00FF84��6#   $
 1Q�65   5      ._�5      �  � RowDefinitions 5      �� 5      $��74 �6$   �� 5      .+�5      �� 
�������$��25(�5!      $��Medium"�5"      $��
Montserrat)�5#      $��Center=�5$      $��0,30,0,0q�62   �� 6E    � Language$ uk-ua�6I   $�Редагування ��6Z   �� 6p   $�товару��6t   6�   �� 5%      -   5         ��Name$ btnLogin��5%      $��0q�5&      $��Редагувати��5'      
�������$��12(�5)      $��
Montserrat)�5*       � Cursor$
 Hand��5+      $��102,391,102,10q�5,      $��1��61   ��5-      �� 5.       �� 
TargetType" ��6    .�5/      ��  �� Property � �� �� Value �	#FF398E4A6;    �� Triggers 50      P� 51   "    P� Property �@J�IsMouseOver P� Value$
 True��6A   . �52   &   ��  � �� �#28AEED6C   53   #   55      ��57      �� 58       �� 
TargetType" ��6*   .R�59      �� $	��150��6%   $��40��61   $ 20��5:   %   #��z��5;   %   ._�5<   "   �� $��Center=�63   $��Center=�5=   5   5>      5?      5A      �� 5C       �� Orientation$ 
Horizontal=�6   $��Center=�5D      $��0,15,0,0q�5E      � 5G      $
��Left=�6   $��5��62   $��	286,5,0,0q�6=   $��1��6P   $	��Top=�6]   $��23��6u   �� 5H      
�������$��12(�5J      $��Medium"�5K      $��
Montserrat)�5L      $
��Wrap=�5M       �� TextAlignment$ Center=�5N      $��138,10,138,432q�5O      $��1��6-   �� 6;   $�Заповн��6?   �� 6O   $�і��6S   �� 6^   $�ть данн��6b   �� 6s   $�і��6w   6�   �� 5P      $��25,28,21,89q�6   $��1��64   $��0��6A   .��5Q      �� .+�5R      �� 
�������$��12(�5T      $��Medium"�5U      $��
Montserrat)�5V      $��0,35,0,0q�5W      �� 6,   $ uk-ua�60   $�
Назва��6A   6Q   �� 5Y      -   5         ��Name$ Product_Name��5Y   "   $��13(�5Z      $��Medium"�5[      $��
Montserrat)�5\      
������� �� 
CaretBrush
 �����
�������$��0,0,0,2q�5`      $��28��5a      $��Center=�5b      $��0,5,0,0q�5c      $��20,0,0,0q�5d      .��6*   ��5e      �� 5f   "    j� Stretch$
 None=�5g       j� 
AlignmentX$
 Left=�5h      .��5i   2   5j      �� 5k      
�������$��12(�5m      $��Medium"�5n      $��
Montserrat)�5o      $��0,35,0,0q�5p      �� 6   $ uk-ua�6   $�Модель��6+   6<   �� 5q      -   5        $ Model��5q   "   $��13(�5r      $��Medium"�5s      $��
Montserrat)�5t      
�������
 �����
�������$��0,0,0,2q�5x      $��28��5y      $��Center=�5z      $��0,5,0,0q�5{      $��20,0,0,0q�5|      .��6   ��5}      �� 5~   "   $
 None=�5      $
 Left=�5�      .��5�   2   5�      �� 5�      $��Категорія��6$   
�������$��12(�5�      $��Medium"�5�      $��
Montserrat)�5�      $��0,35,0,0q�5�      �� 5�      -   5         ��Name$ Category��5�   #   $��13(�5�      $��Medium"�5�      $��
Montserrat)�5�      
���   �
�������$��0,0,0,2q�5�      $��28��5�      $��Center=�5�      $��0,5,0,0q�5�      $��20,0,0,0q�5�      ��6   Z� �� 5�      
�������$��12(�5�      $��Medium"�5�      $��
Montserrat)�5�      $��0,35,0,0q�5�      �� 6*   $ uk-ua�6.   $3�-Технічні характеристики��6?   6a   �� 5�      
�������$��12(�5�      $��Medium"�5�      $��
Montserrat)�5�      $��0,35,0,0q�5�      �� 6   $ uk-ua�6   $�
Екран��6,   6<   �� 5�      -   5        $ Screen��5�   "   $��13(�5�      $��Medium"�5�      $��
Montserrat)�5�      
�������
 �����
�������$��0,0,0,2q�5�      $��28��5�      $��Center=�5�      $��0,5,0,0q�5�      $��20,0,0,0q�5�      .��6(   ��5�      �� 5�   "   $
 None=�5�      $
 Left=�5�      .��5�   2   5�      �� 5�      
�������$��12(�5�      $��Medium"�5�      $��
Montserrat)�5�      $��0,35,0,0q�5�      �� 6   $ uk-ua�6   $�Процесор��6)   6<   �� 5�      -   5        $ 	Processor��5�   "   $��13(�5�      $��Medium"�5�      $��
Montserrat)�5�      
�������
 �����
�������$��0,0,0,2q�5�      $��28��5�      $��Center=�5�      $��0,5,0,0q�5�      $��20,0,0,0q�5�      .��6%   ��5�      �� 5�   "   $
 None=�5�      $
 Left=�5�      .��5�   2   5�      �� 5�      
�������$��12(�5�      $��Medium"�5�      $��
Montserrat)�5�      $��0,35,0,0q�5�      �� 6   $ uk-ua�6   $�Пам'ять��6)   6;   �� 5�      -   5        $ Memory��5�   "   $��13(�5�      $��Medium"�5�      $��
Montserrat)�5�      
�������
 �����
�������$��0,0,0,2q�5�      $��28��5�      $��Center=�5�      $��0,5,0,0q�5�      $��20,0,0,0q�5�      .��6%   ��5�      �� 5�   "   $
 None=�5�      $
 Left=�5�      .��5�   2   5�      �� 5�      
�������$��12(�5�      $��Medium"�5�      $��
Montserrat)�5�      $��0,35,0,0q�5�      �� 6   $ uk-ua�6   $�Камера��6)   6:   �� 5�      -   5        $ Camera��5�   "   $��13(�5�      $��Medium"�5�      $��
Montserrat)�5�      
�������
 �����
�������$��0,0,0,2q�5�      $��28��5�      $��Center=�5�      $��0,5,0,0q�5�      $��20,0,0,0q�5�      .��6%   ��5�      �� 5�   "   $
 None=�5�      $
 Left=�5�      .��5�   2   5�      �� 5      
�������$��12(�5     $��Medium"�5     $��
Montserrat)�5     $��0,35,0,0q�5     �� 6   $ uk-ua�6   $�Батарея��6)   6;   �� 5     -	   5        $ Battery��5  "   $��13(�5     $��Medium"�5     $��
Montserrat)�5	     
�������
 �����
�������$��0,0,0,2q�5     $��28��5     $��Center=�5     $��0,5,0,0q�5     $��20,0,0,0q�5     .��6%   ��5     �� 5  "   $
 None=�5     $
 Left=�5     .��5  2   5     �� 5     
�������$��12(�5     $��Medium"�5     $��
Montserrat)�5     $��0,35,0,0q�5     �� 6   $ uk-ua�6   $�Діагональ��6)   6=   �� 5     -
   5        $ Diagonal��5  "   $��13(�5      $��Medium"�5!     $��
Montserrat)�5"     
�������
 �����
�������$��0,0,0,2q�5&     $��28��5'     $��Center=�5(     $��0,5,0,0q�5)     $��20,0,0,0q�5*     .��6%   ��5+     �� 5,  "   $
 None=�5-     $
 Left=�5.     .��5/  2   50     �� 51     
�������$��12(�53     $��Medium"�54     $��
Montserrat)�55     $��0,35,0,0q�56     �� 6   $ uk-ua�6   $+�%Роздільна здатність��6-   6K   �� 57     -   5        $ 
Resolution��57  "   $��13(�58     $��Medium"�59     $��
Montserrat)�5:     
�������
 �����
�������$��0,0,0,2q�5>     $��28��5?     $��Center=�5@     $��0,5,0,0q�5A     $��20,0,0,0q�5B     .��6   ��5C     �� 5D  "   $
 None=�5E     $
 Left=�5F     .��5G  2   5H     �� 5I     
�������$��12(�5K     $��Medium"�5L     $��
Montserrat)�5M     $��0,35,0,0q�5N     �� 6   $ uk-ua�6   $�Тип дисплею��6-   6C   �� 5O     -   5        $ Display_Type��5O  "   $��13(�5P     $��Medium"�5Q     $��
Montserrat)�5R     
�������
 �����
�������$��0,0,0,2q�5V     $��28��5W     $��Center=�5X     $��0,5,0,0q�5Y     $��20,0,0,0q�5Z     .��6   ��5[     �� 5\  "   $
 None=�5]     $
 Left=�5^     .��5_  2   5`     �� 5a     
�������$��12(�5c     $��Medium"�5d     $��
Montserrat)�5e     $��0,35,0,0q�5f     �� 6   $ uk-ua�6   $(�"Оперативна пам'ять��6-   6J   �� 5g     -   5        $	 RAM��5g  "   $��13(�5h     $��Medium"�5i     $��
Montserrat)�5j     
�������
 �����
�������$��0,0,0,2q�5n     $��28��5o     $��Center=�5p     $��0,5,0,0q�5q     $��20,0,0,0q�5r     .��6   ��5s     �� 5t  "   $
 None=�5u     $
 Left=�5v     .��5w  2   5x     �� 5y     
�������$��12(�5{     $��Medium"�5|     $��
Montserrat)�5}     $��0,35,0,0q�5~     �� 6   $ uk-ua�6   $(�"Об'єм накопичувача��6-   6J   �� 5     -   5        $ Storage��5  "   $��13(�5�     $��Medium"�5�     $��
Montserrat)�5�     
�������
 �����
�������$��0,0,0,2q�5�     $��28��5�     $��Center=�5�     $��0,5,0,0q�5�     $��20,0,0,0q�5�     .��6   ��5�     �� 5�  "   $
 None=�5�     $
 Left=�5�     .��5�  2   5�     �� 5�     
�������$��12(�5�     $��Medium"�5�     $��
Montserrat)�5�     $��0,35,0,0q�5�     �� 6'   $ uk-ua�6+   $�Ціна��6<   6K   �� 5�     -   5        $ Price��5�  "   $��13(�5�     $��Medium"�5�     $��
Montserrat)�5�     
�������
 �����
�������$��0,0,0,2q�5�     $��28��5�     $��Center=�5�     $��0,5,0,0q�5�     $��20,0,0,0q�5�     .��6(   ��5�     �� 5�  "   $
 None=�5�     $
 Left=�5�     .��5�  2   5�     �� 5�     
�������$��12(�5�     $��Medium"�5�     $��
Montserrat)�5�     $��0,35,0,0q�5�     �� 6*   $ uk-ua�6.   $�К��6?   �� 6J   $�ількість��6N   �� 6`   $ uk-ua�6d   $#� товру на складі��6u   6�   �� 5�     -   5        $ Quantity_In_Stock��5�  "   $��13(�5�     $��Medium"�5�     $��
Montserrat)�5�     
�������
 �����
�������$��0,0,0,2q�5�     $��28��5�     $��Center=�5�     $��0,5,0,0q�5�     $��20,0,0,0q�5�     .��6(   ��5�     �� 5�  "   $
 None=�5�     $
 Left=�5�     .��5�  2   5�     �� 5�     $��
Бренд��6$   
�������$��12(�5�     $��Medium"�5�     $��
Montserrat)�5�     $��0,35,0,0q�5�     �� 5�     -   5        $ Brand��5�  #   $��13(�5�     $��Medium"�5�     $��
Montserrat)�5�     
���   �
�������$��0,0,0,2q�5�     $��28��5�     $��Center=�5�     $��0,5,0,0q�5�     $��20,0,0,0q�5�     �� 5�     $)��#Гарантійний період��6$   
�������$��12(�5�  #   $��Medium"�5�  #   $��
Montserrat)�5�  #   $��0,35,0,0q�5�  #   �� 5�     -   5        $ Warranty_Period��5�  "   $��13(�5�  "   $��Medium"�5�  "   $��
Montserrat)�5�  "   
�������
 �����
�������$��0,0,0,2q�5�  "   $��28��5�  "   $��Center=�5�  "   $��0,5,0,0q�5�  "   $��20,0,0,0q�5�  "   .��65   ��5�     �� 5�  "   $
 None=�5�  %   $
 Left=�5�  %   .��5�  2   5�     �� 5�     $��
Колір��6$   
�������$��12(�5�  $   $��Medium"�5�  $   $��
Montserrat)�5�  $   $��0,35,0,0q�5�  $   �� 5�     -   5        $ Color��5�  #   $��13(�5�  #   $��Medium"�5�  #   $��
Montserrat)�5�  #   
�������
�������$��0,0,0,2q�5�  #   $��28��5�  #   $��Center=�5   #   $��0,5,0,0q�5  #   $��20,0,0,0q�5  #   ��5     �� 5  "   $	 0,1&�66   .��5  &   � $	 	#FFF0F0F0��63   � 5  &   $	 Black��63   $
 1Q�6A   5  #   5	     5
     5     �� 5     -   5        $ 
btnClose14��5        IconButtonsStyle#��[  6-    	 close#���  6W   
 � Tag$ IsCloseButton��6�   $��517,10,10,39q�6�   $	��NaN��6�   5     5     5     5     