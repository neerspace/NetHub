const getRandomColor = () => {
  const letters = '0123456789ABCDEF';
  let color = '#';
  for (let i = 0; i < 6; i++) {
    color += letters[Math.floor(Math.random() * 16)];
  }
  return color;
}

const getFirstLetter = (username: string) => username.substring(0, 1);

export const createImageFromInitials = (size: number, username: string) => {
  const letter = getFirstLetter(username)
  const color = getRandomColor();

  const canvas = document.createElement('canvas');
  const context = canvas.getContext('2d');
  canvas.width = canvas.height = size;

  context!.fillStyle = "#ffffff";
  context!.fillRect(0, 0, size, size)

  context!.fillStyle = `${color}50`
  context!.fillRect(0, 0, size, size)

  context!.fillStyle = color;
  context!.textBaseline = 'middle'
  context!.textAlign = 'center'
  context!.font = `${size / 2}px Century Gothic`
  context!.fillText(letter, (size / 2), (size / 2))

  return canvas.toDataURL()
};
