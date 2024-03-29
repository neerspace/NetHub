import React from 'react';
import {Box, Text} from "@chakra-ui/react";
import TeamMemberCard from "../../components/Team/TeamMemberCard";
import {teamMembers} from "../../constants/team";
import Dynamic, { IPage } from "../../components/Dynamic/Dynamic";

export type TeamMember = {
  imageSrc: string,
  fullName: string,
  rank: string,
  icons: { id: string, link: string }[]
}

const TeamSpace: IPage = () => {

  const Titles = {
    Center: <h2>Команда</h2>
  }

  return (
    <Dynamic Titles={Titles}>
      <Box display={'flex'} flexDirection={'column'}>
        <Text as={'p'}>Ми - команда розробників, котрі вирішили створити зручну платформу для написання статтей, курсів
          та багато чого іншого. Власними зусиллями ми розробляємо український проєкт, котрий має стати хабом для, в
          першу чергу, вітчизняних IT-діячів. Сподіваємось, що наші амбіційні плани будуть втілені та знайдуть свого
          користувача. Працюємо на майбутнє нашої неньки України.</Text>
        <Box display={'flex'} mt={4} gap={3} width={'100%'} flexWrap={'wrap'}>
          {
            teamMembers.map(teamMember => <TeamMemberCard key={teamMember.fullName} member={teamMember}/>)
          }
        </Box>
      </Box>
    </Dynamic>
  );
};

TeamSpace.Provider = React.Fragment;
export default TeamSpace;