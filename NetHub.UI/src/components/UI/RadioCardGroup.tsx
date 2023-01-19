import {Box, Text, useRadioGroup} from "@chakra-ui/react";
import RadioCard from "./RadioCard";
import {FC} from "react";

export type RadioGroupConfig = { name: string, defaultValue: string };

interface IRadioCardGroupProps {
  config: RadioGroupConfig,
  onChange: (value: string) => void,
  options: string[]
}

const RadioCardGroup: FC<IRadioCardGroupProps> = ({config, onChange, options}) => {

  const {name, defaultValue} = config;

  const {getRootProps, getRadioProps} = useRadioGroup({
    name,
    defaultValue,
    onChange
  })

  const group = getRootProps()

  return (
    <Box
      display='flex'
      gap={4}
      {...group}
    >
      {options.map((value) => {
        const radio = getRadioProps({value})
        return (
          <RadioCard key={value} {...radio}>
            <Text as={'b'}>{value}</Text>
          </RadioCard>
        )
      })}
    </Box>
  )
}

export default RadioCardGroup;
