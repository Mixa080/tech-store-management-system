   M S B A M L   `   `   `  ����	  Sklep    Sklep.Pages.Dashboard   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        N� Title$  	Dashboard��5      .��5
      ��  �� CornerRadius$ 5��6      SecundaryBackgroundColor#���   6   ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��129* �6#   �� 5      $	��31* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��3* �6    �� 5      $
��361* �6    �� 5      $	��86* �6    .+�5      � $��1��6   $��3��6    $��2��61   $��
10,0,-10,0q�6E    5      �� 5      $	��27* �6$   �� 5      $
��266* �6$   �� 5      $
��157* �6$    5       �� 5!      $	��87* �6'   �� 5"      $
��234* �6'   �� 5#      $	��79* �6'   .+�5&      � $��1,14,0,0q�6   $��2��6)   $��1��6:   $��86��6J   $	��Top=�6V   $��2��6n    5'      �� 5(      $��* �6+   �� 5*      $
��Auto �6+   .+�5.      �� -   5         ��Name$ SearchTextBox��5.      $��1,28,121,22q�65    � ToolTip$  Пошук по назві��6J   $��0��6c   
�������
���   � J� OpacityMask 6�   �� *Clip+ �� ElementName$ SearchTextBox��6�   �� 50      -   5         ��Name$ btnSearch_Copy��50      $��0q�51      $��
Пошук��52      
�������$��12(�54      $��
Montserrat)�55      	 � Cursor$
	 Hand��56      $��0,1,0,1q�57   %   $��Right=�66   $	��118��6R   ��58      �� 59      
 �� 
TargetType"
 ��6$   .�5:   "   ��  �� Property � �� �� Value �	#FF398E4A6?    �� Triggers 5;   "   P� 5<   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5=   *   ��  � �� �#28AEED6G   5>   '   5@      ��5B      �� 5C       �� 
TargetType" ��6.   .R�5D   "   �� $	��100��6)   $��30��65   $ 20��5E      #��z��5F      ._�5G   &   �� $��Center=�67   $��Center=�5H   '   5I   #   5J      5L      5M      �� 5Q      -   5        $ 	btnSearch��5Q      $��0q�5R      $��
Пошук��5S      
�������$��12(�5U      $��
Montserrat)�5V      $
	 Hand��5W      $��
10,53,0,61q�5X      $��2��5Y   1   ��5Z      �� 5[      "
 ��6    .�5\      ��  � �� �	#FF398E4A6;    5]      P� 5^   "    �@J�IsMouseOver$
 True��6A   . �5_   &   ��  � �� �#28AEED6C   5`   #   5b      ��5d      �� 5e      " ��6*   .R�5f      �� $	��150��6%   $��30��61   $ 20��5g   %   #��z��5h   %   ._�5i   "   �� $��Center=�63   $��Center=�5j   3   5k      5l      5n      �� 5q      -   5         ��Name$ ProductsListView��5q      $��	1,74,5,11q�65   $��1��6�   $��1��6�   ���	#FF333C55
�������$��2��6�   $��13(�6  ��6H   �� *
Products+��6�   Z� ��5r      �� 5s      "
 ��6     5t      P� 5u   "    �@��
IsSelected$
 True��6@   . �5v   &   ��  � ��
 �   �6C   5w   #   5y       �� View 5{      �� 5|      .P�5}      �� $��
Назва��6-    �� DisplayMemberBinding 6<   �� *Product_Name+�� 5~      $��Модель��6-    6=   �� *
Model_Name+�� 5      $3��-Технічні характеристики��6-    6N   �� *Description+�� 5�      $��Ціна��6-    6;   �� *Price+�� 5�      $*��$Кількість на складі��6-    6J   �� *Quantity_In_Stock+�� 5�      $��
Бренд��6-    6<   �� *
Brand_Name+�� 5�      $��Категорія��6-    6@   �� *Category_Name+�� 5�      $)��#Гарантійний період��6-    6I   �� *Warranty_Period+�� 5�      $��
Колір��6-    6<   �� *
Color_Name+5�      5�      �� 5�      -   5         ��Name$ TotalAmountTextBlock��5�      $
��Bold"�6�   $��0,0,0,22q�6�   $��2��6�   $��2��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   
�������$��Center=�6�   $	��152��6  $��21��6  $��Bottom=�6*  $��14(�6E  ��6:   ��  �� Path$ TotalAmount� �� StringFormat$&  Загальна сума: {0:C}��z�5�      V� 5�      .~�5�      ��  �� ScaleX$ 1Q�6-   �� 5�      �� 5�      T� 5�      5�      5�      �� 5�      -   5        $ basket��5�      $��	3,73,3,60q�6+   $��2��6�   $��1��6�   ���	#FF333C55
�������$��2��6�   $��13(�6�   ��6>   �� *
Products+��6�   Z� ��5�      �� 5�      "
 ��6     5�      P� 5�   "    �@��
IsSelected$
 True��6@   . �5�   &   ��  � ��
 �   �6C   5�   #   5�       5�      �� 5�      .P�5�      �� $��
Назва��6-    6<   �� *Product_Name+�� 5�      $��Модель��6-    6=   �� *
Model_Name+�� 5�      $��Ціна��6-    6;   �� *Price+�� 5�      $��Кількість��6-    6@   �� *
Quantity+5�      5�      �� 5�      $��10,28,21,40q�6   $��1��62   $��0��6?   .+�5�      �� .+�5�      �� $ ��Діапазон ціни:��6$   
�������$��12(�5�   $   $��Medium"�5�   $   $��
Montserrat)�5�   $   $��0,5,0,0q�5�   $   �� 5�      $	��169��6%   .+�5�      �� $��Від��6(   
�������$��12(�5�   (   $��Medium"�5�   (   $��
Montserrat)�5�   (   $��0,5,0,0q�5�   (   �� 5�      -   5        $ txtMinPrice��5�   &   $��13(�5�   &   $��Medium"�5�   &   $��
Montserrat)�5�   &   
������� �� 
CaretBrush
 �����
�������$��0,0,0,2q�5�   &   $��28��5�   &   $��Center=�5�   &   $��0,5,0,0q�5�   &   $��20,0,0,0q�5�   &   .��69   ��5�   "   �� 5�   &    j� Stretch$
 None=�5�   )    j� 
AlignmentX$
 Left=�5�   )   .��5�   6   5�      �� 5�      $
��до��6(   
�������$��12(�5�   (   $��Medium"�5�   (   $��
Montserrat)�5�   (   $��0,5,0,0q�5�   (   �� 5�      -   5        $ txtMaxPrice��5�   &   $��13(�5�   &   $��Medium"�5�   &   $��
Montserrat)�5�   &   
�������
 �����
�������$��0,0,0,2q�5�   &   $��28��5�   &   $��Center=�5�   &   $��20,0,0,0q�5�   &   .��69   ��5�   "   �� 5�   &   $
 None=�5�   )   $
 Left=�5�   )   .��5�   6   5�      5�      �� 5�      -	   5         ��Name$ brandsCheckBoxPanel��5�   %   .+�5�      �� $
��Bold"�6(   $��Бренди:��6:     PrimaryTextColor#���  6I   �� 5�      -
   5         ��Name$ cmbBrand��5�   '   
���   �69   5�      �� 5�      -   5        $ colorsCheckBoxPanel��5�   %   .+�5�      �� $
��Bold"�6(   $��Кольори:��6:   #���  6J   �� 5�      -   5        $ cmbColor��5�   '   
���   �69   5�      �� 5�      -   5        $ categoriesCheckBoxPanel��5�   %   .+�5�      �� $
��Bold"�6(   $��Категорії:��6:   #���  6L   �� 5�      -   5        $ cmbCategory��5�   '   
���   �6<   5�      5�      5�      �� 5�      -   5        $ btnOrder��5�      $��0q�5�      $+��%Оформити замовлення��5�      
�������$��12(�5�      $��
Montserrat)�5�      $
	 Hand��5      $��	10,91,0,8q�5     $��2��5  0   ��5     �� 5     "
 ��6    .�5     ��  � �� �	#FF398E4A6;    5     P� 5  "    �@J�IsMouseOver$
 True��6A   . �5  &   ��  � �� �#28AEED6C   5	  #   5     ��5     �� 5     " ��6*   .R�5     �� $	��150��6%   $��30��61   $ 20��5  %   #��z��5  %   ._�5  "   �� $��Center=�63   $��Center=�5  3   5     5     5     �� 5     $
��Left=�6   $��
249,49,0,0q�67   $	��Top=�6K   $��18��6c   $��1��6o   $��0��6   $��1��6�   �� 5     -   5        $ btninfo��5     $��0q�5     $��Інформація��5     
�������$��12(�5     $��
Montserrat)�5     $
	 Hand��5      $��11,10,0,109q�5!     $��2��5"  +   ��5#     �� 5$     "
 ��6    .�5%     ��  � �� �	#FF398E4A6;    5&     P� 5'  "    �@J�IsMouseOver$
 True��6A   . �5(  &   ��  � �� �#28AEED6C   5)  #   5+     ��5-     �� 5.     " ��6*   .R�5/     �� $	��150��6%   $��30��61   $ 20��50  !   #��z��51  !   ._�52  "   �� $��Center=�63   $��Center=�53  %   54     55     57     59     5:     5;     5<     