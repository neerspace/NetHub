import React, {FC} from 'react';
import {Card, CardBody, CardFooter, Heading, Icon, Image, Link, Text, useColorModeValue} from "@chakra-ui/react";
import {TeamMember} from "../../pages/Team/TeamSpace";
import SvgSelector from "../UI/SvgSelector/SvgSelector";
import cl from './TeamMemberCard.module.sass';

interface ITeamMemberCardProps {
  member: TeamMember
}

const TeamMemberCard: FC<ITeamMemberCardProps> = ({member}) => {

  const cardBg = useColorModeValue('violetLight', '#323232')
  const textColor = useColorModeValue('#1A1A1A', 'whiteDark')
  const svgColor = useColorModeValue(cl.cardFooterWhite, cl.cardFooterBlack)

  return (
    <Card bg={cardBg} padding={'12px'} maxW={'176px'} boxShadow={0} borderRadius={'12px'}>
      <CardBody display={'flex'} flexDirection={'column'} pt={0} pb={2} px={0}>
        <Image
          boxSize={'152px'}
          src={member.imageSrc}
          objectFit={'cover'}
          alt='Member photo'
          borderRadius='lg'
        />
        <Heading mt={2} size='md' color={textColor}>{member.fullName}</Heading>
        <Text color={textColor} as={'p'}>{member.rank}</Text>
      </CardBody>
      <CardFooter display={'flex'} py={0} px={0} gap={2} className={svgColor}>
        {
          member.icons.map(icon => <Link key={icon.id} href={icon.link} target={'_blank'}><SvgSelector
            id={icon.id}
          /></Link>)
        }
      </CardFooter>
    </Card>
  );
};

export default TeamMemberCard;