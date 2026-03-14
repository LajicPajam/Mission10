import type { Bowler } from '../types/Bowler'

type BowlerTableProps = {
  bowlers: Bowler[]
}

const formatName = (bowler: Bowler) => {
  // Middle initial is optional in the source data.
  const middle = bowler.bowlerMiddleInit ? ` ${bowler.bowlerMiddleInit}.` : ''
  return `${bowler.bowlerFirstName}${middle} ${bowler.bowlerLastName}`
}

// Keep the table readable when a nullable DB field is missing.
const valueOrDash = (value: string | null) => value ?? '-'

const BowlerTable = ({ bowlers }: BowlerTableProps) => {
  return (
    <section>
      <table aria-label="Bowlers table">
        <thead>
          <tr>
            <th>Bowler Name</th>
            <th>Team Name</th>
            <th>Address</th>
            <th>City</th>
            <th>State</th>
            <th>Zip</th>
            <th>Phone Number</th>
          </tr>
        </thead>
        <tbody>
          {bowlers.map((bowler) => (
            <tr key={`${bowler.bowlerFirstName}-${bowler.bowlerLastName}-${bowler.teamName}`}>
              <td>{formatName(bowler)}</td>
              <td>{bowler.teamName}</td>
              <td>{valueOrDash(bowler.bowlerAddress)}</td>
              <td>{valueOrDash(bowler.bowlerCity)}</td>
              <td>{valueOrDash(bowler.bowlerState)}</td>
              <td>{valueOrDash(bowler.bowlerZip)}</td>
              <td>{valueOrDash(bowler.bowlerPhoneNumber)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  )
}

export default BowlerTable
