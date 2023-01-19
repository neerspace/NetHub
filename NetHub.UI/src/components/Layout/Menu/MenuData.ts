export interface IMenuItem {
  svgId: string,
  itemName: string,
  link: string,
  isActive: boolean
}

export const firstGroup: IMenuItem[] = [
  {
    svgId: 'Globe',
    itemName: 'Стрічка',
    link: '/',
    isActive: true
  },
  {
    svgId: 'StarCircle',
    itemName: 'Рекомендації',
    link: '/for-you',
    isActive: false
  }
]

export const secondGroup: IMenuItem[] = [
  {
    svgId: 'MenuSaved',
    itemName: 'Збережено',
    link: '/saved',
    isActive: true
  },
  {
    svgId: 'Draw',
    itemName: 'Створено вами',
    link: '/by-you',
    isActive: true
  },
  {
    svgId: 'Send',
    itemName: 'Підписки',
    link: '/subscriptions',
    isActive: false
  }];

export const thirdGroup: IMenuItem[] = [

  {
    svgId: 'Profile',
    itemName: 'Налаштування',
    link: '/profile',
    isActive: true
  },
  {
    svgId: 'Settings',
    itemName: 'Тестова сторінка',
    link: '/test',
    isActive: true
  }
];
