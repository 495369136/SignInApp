   M S B A M L   `   `   `  ���� D  @SignInApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SignInApp	SignInApp  7 clr-namespace:SignInApp.ControlsSignInApp.Controls  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TabItem.Static.Background  �� EndPoint �� 
StartPoint � Color � Offset   TabItem.MouseOver.Background  TabItem.Static.Border �� Color  TabItem.MouseOver.Border  TabItem.Disabled.Background  TabItem.Disabled.Border  TabItem.Selected.Border  TabItem.Selected.Background  CloseTabItem �� 
TargetType �� Property �� Value �� 
TargetType	 �Name
 J� SnapsToDevicePixels ��Name �� CornerRadius � ColumnDefinitions ��Name �� RelativeSource �� AncestorType$    SignInApp.Controls.ImageButton   Name    	ImagePath �� Triggers ^� 
Conditions �� Binding �� Value �� 
TargetName 	 ImageTableItem �� Orientation�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     !localclr-namespace:SignInApp  -controls clr-namespace:SignInApp.Controls  %L  &	        &	 V     &	 �     &	 �     &	 �     &	 �     &	 �     &	     &	     7   7  7  7  7  &		 �    7   7  7  7  7  �� $	  0,1&�$	 0,0&�.��� $ #F0F0F0��$	 0.0Q�� $ #E5E5E5��$	 1.0Q��� $	  0,1&�$	 0,0&�.��� $ #ECF4FC��$	 0.0Q�� $ #DCECFC��$	 1.0Q��� $ #ACACAC���� $ #7EB4EA���� $ #F0F0F0���� $ #D9D9D9���� $ #ACACAC���� $ #FFFFFF���� " ��.���  � �� �#1890FF��  � ��$ 60����  � ��$
 Auto����  � ��$	 400����  � ��$ 18(���  � ��$ 思源黑体 CN Medium)���  � ��8   ��  � ��8  ��  � ��$ 0q���  � ��$ 	26,2,13,2q���  � ��$ Stretch=���  � ��$ Stretch=���  � �� �� " ��.R�� $	 templateRoot��
 . .+��� $ 
mainBorder��$��0,0,1,0q�#��z��$��0q�$ 5,5,0,0���  �� $��* ��� $��50 �.+��� $ contentPresenter��$��0��$��Header��|�.  $��30,0,0,0q���. #
 z
 ���� *HorizontalContentAlignment+ �� " ������ *VerticalContentAlignment+ �� " ��   $ tabCloseButton��$��1��$��14��$ \Images\close.png��
������ $��	18,0,20,0q����� *HorizontalContentAlignment+ �� " �� ^�  ��  �� *IsMouseOver+ �� *Self+ true��  �� *TabStripPlacement+ �� " �� Top.0��� $ 
mainBorder��8   � ���� $ 
mainBorder�� � ��
 �����^�  ��  �� *	IsEnabled+ �� *Self+	 false��  �� *TabStripPlacement+ �� " �� Top.0��� $ contentPresenter�� �@J�Opacity$
 0.56Q��� $ 
mainBorder��8   � ���� $ 
mainBorder��8   � ��^�  ��  �� *
IsSelected+ �� *Self+	 false��  �� *TabStripPlacement+ �� " �� Top.0��� $ 
mainBorder�� � ��$ 0,0,1,0q��� $ 
mainBorder�� �@��CornerRadius$ 5,5,0,0��^�  ��  �� *
IsSelected+ �� *Self+ true��  �� *TabStripPlacement+ �� " �� Top.0���  �@K�ZIndex$ 1���� $ 
mainBorder�� � ��
 ������� " ��.���  � �� �#1890FF��  � ��$ 60����  � ��$
 Auto����  � ��$ 18(���  � ��$ 思源黑体 CN Medium)���  � ��8   ��  � ��8  ��  � ��$ Stretch=���  � ��$ Stretch=���  � �� �� " ��.R�� $	 templateRoot��
 . .+��� $ 
mainBorder��$��0,0,1,0q�#��z��$ 5,5,0,0���� $ 
Horizontal=�$��Center=�.+��� $��\Images\class.png��$��21��$��Uniform=�$��40,0,0,0q��� $ contentPresenter��$��Header��|�.  $��	15,0,40,0q���. #
 z
 ���� *HorizontalContentAlignment+ �� " ������ *VerticalContentAlignment+ �� " �� ^�  ��  �� *IsMouseOver+ �� *Self+ true��  �� *TabStripPlacement+ �� " �� Top.0��� $ 
mainBorder��8   � ���� $ 
mainBorder�� � ��
 �����^�  ��  �� *	IsEnabled+ �� *Self+	 false��  �� *TabStripPlacement+ �� " �� Top.0��� $ contentPresenter�� �@J�Opacity$
 0.56Q��� $ 
mainBorder��8   � ���� $ 
mainBorder��8   � ��^�  ��  �� *
IsSelected+ �� *Self+	 false��  �� *TabStripPlacement+ �� " �� Top.0��� $ 
mainBorder�� � ��$ 0,0,1,0q��� $ 
mainBorder�� �@��CornerRadius$ 5,5,0,0��^�  ��  �� *
IsSelected+ �� *Self+ true��  �� *TabStripPlacement+ �� " �� Top.0���  �@K�ZIndex$ 1���� $ 
mainBorder�� � ��
 �����